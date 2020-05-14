namespace RPGCalendar.Identity
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class SqliteAuthenticationDbContext : AuthenticationDbContext
    { 

        public SqliteAuthenticationDbContext(IConfiguration configuration)
        :base(configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("AuthConnection"));
        }
    }
}
