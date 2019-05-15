using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HealthCheck.API.Services
{
    public class CategoryRepository
    {
        private readonly DatabaseContext databaseContext;

        public CategoryRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IEnumerable<Category> GetAll()
        {
            return databaseContext.Categories.ToList();
        }

        public IEnumerable<Category> GetCategories(Expression<Func<Category, bool>> where)
        {
            return databaseContext.Categories.Where(where);
        }

        public IEnumerable<Category> GetCategories(IEnumerable<int> ids)
        {
            var listOfCategoryIds = databaseContext.Categories.Select(s => s.CategoryId);
            return databaseContext.Categories.Where(c => listOfCategoryIds.Contains(c.CategoryId));
        }

        public Category GetById(int id)
        {
            return databaseContext.Categories.SingleOrDefault(x => x.CategoryId == id);
        }

        public Category SingleOrDefault(Expression<Func<Category, bool>> where)
        {
            return databaseContext.Categories.SingleOrDefault(where);
        }
        public Category Create(Category category)
        {
            var persistedCategory = databaseContext.Categories.Add(category);
            SaveChanges();
            return persistedCategory.Entity;
        }

        public Category Update(Category category)
        {
            databaseContext.Categories.Update(category);
            SaveChanges();
            return category;
        }

        public void Delete(Category category)
        {
            databaseContext.Categories.Remove(category);
            SaveChanges();
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}
