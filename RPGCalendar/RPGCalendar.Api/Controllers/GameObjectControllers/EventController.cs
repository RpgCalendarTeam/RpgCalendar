
namespace RPGCalendar.Api.Controllers.GameObjectControllers
{
    using Core.Dto;
    using Core.Services;

    public class EventController : BaseApiController<Event, EventInput>
    {
        public EventController(IEventService service) :
            base(service)
        { }
    }
}