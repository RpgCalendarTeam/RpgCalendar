namespace RPGCalendar.Core.Services
{
    using AutoMapper;
    using Data.GameCalendar;
    using Repositories;

    public interface ICalendarService : IEntityService<Dto.Calendar, Dto.CalendarInput>
    {
    }
    public class CalendarService : EntityService<Dto.Calendar, Dto.CalendarInput, Calendar, ICalendarRepository>, ICalendarService
    {
        public CalendarService(IMapper mapper, ICalendarRepository calendarRepository)
            : base(mapper, calendarRepository)
        {
        }
    }
}
