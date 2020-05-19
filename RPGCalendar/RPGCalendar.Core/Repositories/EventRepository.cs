namespace RPGCalendar.Core.Repositories
{
    using Data;
    using Data.GameObjects;

    public interface IEventRepository : IEntityRepository<Event>
    {
    }
    public class EventRepository : EntityRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
    
}
