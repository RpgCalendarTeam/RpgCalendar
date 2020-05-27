﻿namespace RPGCalendar.Api.Controllers.GameObjectControllers
{
    using Core.Dto;
    using Core.Services;

    public class NotificationController : BaseApiController<Notification, NotificationInput>
    {
        public NotificationController(INotificationService service) :
            base(service)
        { }
    }
}
