namespace RPGCalendar.Core.Repositories
{
    using Data;
    using Data.GameCalendar;

    public interface ICalendarRepository : IEntityRepository<Calendar>
    {
    }
    public class CalendarRepository : EntityRepository<Calendar>, ICalendarRepository
    {
        public CalendarRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
