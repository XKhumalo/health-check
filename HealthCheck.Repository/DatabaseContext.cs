using HealthCheck.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthCheck.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<GuestUserAnswer> GuestUserAnswers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionCategory> SessionCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SessionOnlyUser> SessionOnlyUsers { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
    }
}
