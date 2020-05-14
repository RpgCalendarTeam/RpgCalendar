namespace RPGCalendar.Data
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using GameObjects;
    using Joins;
    using GameCalendar;
    using Microsoft.Extensions.Configuration;

    public class ApplicationDbContext : DbContext
    {
#nullable disable
        public IConfiguration Configuration { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> GameNotes { get; set; }
        public DbSet<Event> GameEvents { get; set; }
        public DbSet<Item> GameItems { get; set; }
        public DbSet<Notification> GameNotifications { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        
        private IHttpContextAccessor HttpContextAccessor { get; set; }
#nullable enable

        public ApplicationDbContext(IConfiguration configuration) 
            : base()
        {
            Configuration = configuration;
        }

        public ApplicationDbContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
            : this(configuration)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        protected void OnCalendarCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calendar>()
                .Property(e => e.Months)
                .HasConversion(
                    v => string.Join(',', v!),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Calendar>()
                .Property(e => e.Days)
                .HasConversion(
                    v => string.Join(',', v!),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameUser>()
                .HasKey(t => new { t.GameId, t.UserId });

            modelBuilder.Entity<GameUser>()
                .HasOne(pt => pt.Game)
                .WithMany(p => p.GameUsers)
                .HasForeignKey(pt => pt.GameId);

            modelBuilder.Entity<GameUser>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.GameUsers)
                .HasForeignKey(pt => pt.UserId);
            OnCalendarCreating(modelBuilder);
            //    _ = modelBuilder?.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });

        }

        public override int SaveChanges()
        {
            AddFingerPrinting();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddFingerPrinting();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddFingerPrinting()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            var added = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (EntityEntry entry in added)
            {
                if (entry.Entity is FingerPrintEntityBase fingerPrintEntry)
                {
                    fingerPrintEntry.CreatedOn = DateTime.UtcNow;
                    fingerPrintEntry.CreatedBy = string.Empty; //HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? string.Empty;
                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = string.Empty; // HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? string.Empty;
                }
            }

            foreach (EntityEntry entry in modified)
            {
                if (entry.Entity is FingerPrintEntityBase fingerPrintEntry)
                {
                    ResetValue(entry, nameof(FingerPrintEntityBase.CreatedOn));
                    ResetValue(entry, nameof(FingerPrintEntityBase.CreatedBy));

                    fingerPrintEntry.ModifiedOn = DateTime.UtcNow;
                    fingerPrintEntry.ModifiedBy = string.Empty; // HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value ?? string.Empty;
                }
            }
        }

        private static void ResetValue(EntityEntry entry, string propertyName)
        {
            PropertyEntry property = entry.Property(propertyName);
            property.CurrentValue = property.OriginalValue;
        }
    }
}