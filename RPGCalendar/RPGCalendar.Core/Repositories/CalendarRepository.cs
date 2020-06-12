namespace RPGCalendar.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.GameCalendar;
    using Microsoft.EntityFrameworkCore;

    public interface ICalendarRepository : IEntityRepository<Calendar>
    {

    }
    public class CalendarRepository : EntityRepository<Calendar>, ICalendarRepository
    {
        public CalendarRepository(ApplicationDbContext dbContext) : 
            base(dbContext)
        {
        }

        public override async Task<List<Calendar>> FetchAllAsync() =>
            await Query.Include(e => e.Months)
                .Include(e => e.DaysOfWeek)
                .ToListAsync();

        public override async Task<Calendar?> FetchByIdAsync(int id)
        {
            return await Query.Include(e => e.Months)
                .Include(e => e.DaysOfWeek)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
