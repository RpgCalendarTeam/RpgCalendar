﻿namespace RPGCalendar.Core
{
    using System.Reflection;
    using AutoMapper;
    using Data;

    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Dto.GameNoteInput, GameNote>();
            CreateMap<GameNote, Dto.GameNote>();
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