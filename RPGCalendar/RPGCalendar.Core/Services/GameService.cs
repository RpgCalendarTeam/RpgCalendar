namespace RPGCalendar.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Exceptions;
    using Data.Joins;
    using Dto;
    using Exceptions;
    using Repositories;
    using Calendar = Data.GameCalendar.Calendar;
    using Game = Data.Game;
    using User = Data.User;

    public interface IGameService
    {
        Task<Dto.Game?> AddNew(int gameId);
        Task<Dto.Game?> AddNew(int gameId, string playerClass, string playerBio);
        Task<Dto.Game?> CreateAsync(Dto.GameInput input);
        Task<List<Dto.Game>> GetForUserAsync();
        Task<Dto.Game?> GetByIdForUserAsync(int id);
        Task<Dto.Game?> UpdateAsync(int id, Dto.GameInput entity);
        Task<bool> DeleteAsync(int id);
        Task<Game?> GetById(int gameId);

    }
    public class GameService : IGameService
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IGameRepository _gameRepository;
        private readonly ICalendarRepository _calendarRepository;

        public GameService(ISessionService sessionService, IMapper mapper, IUserService userService, IGameRepository gameRepository, ICalendarRepository calendarRepository)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _userService = userService;
            _gameRepository = gameRepository;
            _calendarRepository = calendarRepository;
        }

        public async Task<List<Dto.Game>> GetForUserAsync()
        {
            var userId = _sessionService.GetCurrentUserId();
            var games = (await _gameRepository.FetchAllAsync()).Where(e => e.IsInGame(userId));
            return _mapper.Map<List<Game>, List<Dto.Game>>(games.ToList());
        }
        public async Task<Dto.Game?> GetByIdForUserAsync(int id)
        {
            var userId = _sessionService.GetCurrentUserId();
            var game = await GetById(id);
            if (game is null)
                return null;
            if(!game.IsInGame(userId))
                throw new UserPermissionException("Read Permission Denied");
            _sessionService.SetCurrentGameId(game.Id);
            return _mapper.Map<Game, Dto.Game>(game);
        }

        public async Task<Dto.Game?> CreateAsync(Dto.GameInput input)
        {
            Game entity = _mapper.Map<Dto.GameInput, Game>(input);
            int userId = _sessionService.GetCurrentUserId();
            User user = await _userService.GetUserById(userId) ?? throw new IllegalStateException(nameof(User));
            entity.GameMaster = user.Id;
            entity.GameUsers.Add(new GameUser(user.Id, user, entity.Id, entity));
            await _gameRepository.InsertAsync(entity);
            Calendar calendar = _mapper.Map<Calendar>(input.Calendar);
            calendar.Game = entity;
            calendar.GameId = entity.Id;
            await _calendarRepository.InsertAsync(calendar);
            entity.Calendar = calendar;
            entity.CalendarId = calendar.Id;
            await _gameRepository.UpdateAsync(entity.Id, entity);
            _sessionService.SetCurrentGameId(entity.Id);
            return _mapper.Map<Game, Dto.Game>(entity);
        }

        public async Task<Dto.Game?> UpdateAsync(int id, Dto.GameInput input)
        {
            Game entity = _mapper.Map<Dto.GameInput, Game>(input);
            Game? result = await _gameRepository.UpdateAsync(id, entity);
            return result is null 
            ? null
            :_mapper.Map<Game, Dto.Game>(result);
        }

        public Task<bool> DeleteAsync(int id)
            => _gameRepository.DeleteAsync(id);

        public async Task<Dto.Game?> AddNew(int gameId)
        {
            var userId = _sessionService.GetCurrentUserId();
            User user = await _userService.GetUserById(userId) ?? throw new IllegalStateException(nameof(User));

            var game = await _gameRepository.AddNewGame(gameId, user);

            if (game is null)
                return null;

            _sessionService.SetCurrentGameId(game.Id);
            return _mapper.Map<Game, Dto.Game>(game);
        }

        public async Task<Dto.Game?> AddNew(int gameId, string playerClass, string playerBio)
        {
            var userId = _sessionService.GetCurrentUserId();
            User user = await _userService.GetUserById(userId) ?? throw new IllegalStateException(nameof(User));

            var game = await _gameRepository.AddNewGame(gameId, user, playerClass, playerBio);

            if (game is null)
                return null;

            _sessionService.SetCurrentGameId(game.Id);
            return _mapper.Map<Game, Dto.Game>(game);
        }

        public Task<Game?> GetById(int gameId)
            => _gameRepository.FetchByIdAsync(gameId);
    }
}
