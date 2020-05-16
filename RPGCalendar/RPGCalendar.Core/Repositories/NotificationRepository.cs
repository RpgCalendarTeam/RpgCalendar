namespace RPGCalendar.Core.Repositories
{
    using Data;
    using Data.GameObjects;

    public interface INotificationRepository : IEntityRepository<Notification>
    {

    }

    public class NotificationRepository : EntityRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
