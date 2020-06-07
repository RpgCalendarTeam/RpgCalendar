namespace RPGCalendar.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Dto;
    using Repositories;
    using Calendar = Data.GameCalendar.Calendar;

    public interface ITimeService
    {
        Task<Dto.Calendar?> ProceedTime(TimeChange timeChange);
    }

    public class TimeService : ITimeService
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IGameService _gameService;
        private readonly ICalendarRepository _calendarRepository;


        public TimeService(IMapper mapper, ISessionService sessionService, IGameService gameService, ICalendarRepository calendarRepository)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _gameService = gameService;
            _calendarRepository = calendarRepository;
        }


        public async Task<Dto.Calendar?> ProceedTime(TimeChange timeChange)
        {

            var game = await _gameService.GetById(_sessionService.GetCurrentGameId());
            Calendar? calendar = await _calendarRepository.FetchByIdAsync(game!.CalendarId);
            if (calendar == null) throw new NullReferenceException(nameof(calendar));
            if (timeChange.SetTime != null) 
                calendar.CurrentTime = timeChange.SetTime.Value;
            else
            {
                calendar.addSeconds(timeChange.Seconds ?? 0);
                calendar.AddMins(timeChange.Minutes ?? 0);
                calendar.AddHours(timeChange.Hours ?? 0);
                calendar.AddDays(timeChange.Days ?? 0);
                calendar.AddYears(timeChange.Years ?? 0);
            }
            await _calendarRepository.UpdateAsync(calendar.Id, calendar);
            return _mapper.Map<Dto.Calendar>(calendar);
        }
    }
}