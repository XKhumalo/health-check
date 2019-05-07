using HealthCheck.Model;
using HealthCheck.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthCheck.API.Services
{
    public class CategoryService
    {
        private readonly IEFRepository<Category> repository;
        private readonly DatabaseContext databaseContext;

        public CategoryService(IEFRepository<Category> repository, DatabaseContext databaseContext)
        {
            this.repository = repository;
            this.databaseContext = databaseContext;
        }

        public async Task<Category> Create(Category category)
        {
            return await repository.Create(category);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await repository.GetAll();
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

        public async Task<Category> SingleOrDefault(Expression<Func<Category, bool>> where)
        {
            return await repository.SingleOrDefault(where);
        }

        public void Delete(Category category)
        {
            repository.Delete(category);
        }

        public async Task<Category> Update(Category category)
        {
            await repository.Update(category);
            return category;
        }
    }
}
