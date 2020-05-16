namespace RPGCalendar.Core.Services
{
    using AutoMapper;
    using Data.GameObjects;
    using Repositories;

    public interface IEventService : IGameObjectService<Dto.Event, Dto.EventInput>
    {
    }
    public class EventService : GameObjectService<Dto.Event, Dto.EventInput, Event, IEventRepository>, IEventService
    {
        public EventService(IMapper mapper, ISessionService sessionService, IGameService gameService, IEventRepository eventRepository)
            : base(mapper, sessionService, gameService, eventRepository)
        {
        }
    }
    
}
