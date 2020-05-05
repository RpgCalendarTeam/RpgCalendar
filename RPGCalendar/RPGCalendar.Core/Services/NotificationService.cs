﻿namespace RPGCalendar.Core.Services
{
    using AutoMapper;
    using Data;
    using Data.GameObjects;

    public interface INotificationService : IGameObjectService<Dto.Notification, Dto.NotificationInput>
    {

    }

    public class NotificationService : GameObjectService<Dto.Notification, Dto.NotificationInput, Notification>, INotificationService
    {
        public NotificationService(ApplicationDbContext dbContext, IMapper mapper, ISessionService sessionService, IGameService gameService)
            : base(dbContext, mapper, sessionService, gameService)
        {
        }
    }
}
