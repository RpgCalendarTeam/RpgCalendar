namespace RPGCalendar.Core.Services
{
    using AutoMapper;
    using Data.GameObjects;
    using Repositories;

    public interface INotificationService : IGameObjectService<Dto.Notification, Dto.NotificationInput>
    {

    }

    public class NotificationService : GameObjectService<Dto.Notification, Dto.NotificationInput, Notification, INotificationRepository>, INotificationService
    {
        public NotificationService(IMapper mapper, ISessionService sessionService, IGameService gameService, INotificationRepository notificationRepository)
            : base(mapper, sessionService, gameService, notificationRepository)
        {
        }
    }
}
