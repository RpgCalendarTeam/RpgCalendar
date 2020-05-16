namespace RPGCalendar.Controllers
{
    using Core.Dto;
    using Core.Services;

    public class CalendarController : BaseApiController<Calendar, CalendarInput>
    {
        public CalendarController(ICalendarService service) :
            base(service)
        { }
    }
}
