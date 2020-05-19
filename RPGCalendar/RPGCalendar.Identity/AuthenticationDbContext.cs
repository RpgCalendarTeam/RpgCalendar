namespace RPGCalendar.Identity
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class AuthenticationDbContext : IdentityDbContext<ApplicationUser>
    { 
        protected readonly IConfiguration Configuration;

        public AuthenticationDbContext(IConfiguration configuration)
        :base()
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AuthConnection"));
        }
    }
}
