namespace RPGCalendar.Data
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class SqliteDbContext : ApplicationDbContext
    {
        public SqliteDbContext(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public SqliteDbContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
            : base(configuration, httpContextAccessor)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
