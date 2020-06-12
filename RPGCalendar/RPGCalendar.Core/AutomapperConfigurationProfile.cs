namespace RPGCalendar.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using Data;
    using Data.GameObjects;
    using Data.GameCalendar;

    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Dto.NoteInput, Note>();
            CreateMap<Note, Dto.Note>();

            CreateMap<Dto.EventInput, Event>();
            CreateMap<Event, Dto.Event>();

            CreateMap<Dto.ItemInput, Item>();
            CreateMap<Item, Dto.Item>();

            CreateMap<Dto.NotificationInput, Notification>();
            CreateMap<Notification, Dto.Notification>();

            CreateMap<Dto.GameInput, Game>().ForMember(des => des.Calendar, 
                opt => opt.Ignore());
            CreateMap<Game, Dto.Game>();

            CreateMap<Dto.UserInput, User>();
            CreateMap<User, Dto.User>();

        }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
