namespace RPGCalendar.Core.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.GameCalendar;
    public interface ITimeService
    {
        Task<Calendar?> FetchByIdAsync(int id);
        Task<Calendar?> ProceedTime(int id, long second);
    }

    public class TimeService : ITimeService
    {
        protected ApplicationDbContext DbContext { get; }

        protected IMapper Mapper { get; }

        protected virtual IQueryable<Calendar> Query => DbContext.Set<Calendar>();

        public TimeService(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async Task<Calendar?> FetchByIdAsync(int id)
        {
            return Mapper.Map<Calendar>(await Query.FirstOrDefaultAsync(x => x.Id == id));
        }

        public virtual async Task<Calendar?> ProceedTime(int id, long sec)
        {
            Calendar? calendar = await FetchByIdAsync(id);
            if (calendar == null) throw new NullReferenceException(nameof(calendar));
            calendar.addTime(sec);
            await DbContext.SaveChangesAsync();
            return calendar;
        }
    }
}