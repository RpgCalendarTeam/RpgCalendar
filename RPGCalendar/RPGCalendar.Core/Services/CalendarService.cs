namespace RPGCalendar.Core.Services
{
    using AutoMapper;
    using Data;
    using Data.GameCalendar;

    public interface ICalendarService : IEntityService<Dto.Calendar, Dto.CalendarInput>
    {
    }
    public class CalendarService : EntityService<Dto.Calendar, Dto.CalendarInput, Calendar>, ICalendarService
    {
        public CalendarService(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
