namespace RPGCalendar.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Data.Exceptions;
    using Data.Joins;
    using Exceptions;
    using Microsoft.EntityFrameworkCore;

    public interface IGameService : IEntityService<Dto.Game, Dto.GameInput>
    {
        Task<Dto.Game?> AddNewGame(int gameId);
        Task<Game> GetGameById(int gameId);
    }
    public class GameService : EntityService<Dto.Game, Dto.GameInput, Game>, IGameService
    {
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly ICalendarService _calendarService;

        public GameService(ISessionService sessionService, ApplicationDbContext dbContext, IMapper mapper, IUserService userService, ICalendarService calendarService)
            : base(dbContext, mapper)
        {
            _sessionService = sessionService;
            _userService = userService;
            _calendarService = calendarService;
        }

        public override async Task<List<Dto.Game>> FetchAllAsync()
        {
            var userId = _sessionService.GetCurrentUserId();
            var gamesQuery = Query.Include(g => g.GameUsers).Include(g => g.GameTime);

            var games = gamesQuery.Where(e => e.GameUsers.Any(g => g.UserId == userId));
            return Mapper.Map<List<Game>, List<Dto.Game>>(await games.ToListAsync());
        }
        public override async Task<Dto.Game?> FetchByIdAsync(int id)
        {
            var userId = _sessionService.GetCurrentUserId();
            var game = await GetGameById(id);
            if(!game.IsInGame(userId))
                throw new UserPermissionException("Read Permission Denied");
            _sessionService.SetCurrentGameId(game.Id);
            return Mapper.Map<Game, Dto.Game>(game);
        }

        public override async Task<Dto.Game?> InsertAsync(Dto.GameInput dto)
        {
            Game entity = Mapper.Map<Dto.GameInput, Game>(dto);
            int userId = _sessionService.GetCurrentUserId();
            User user = await _userService.GetUserById(userId) ?? throw new IllegalStateException(nameof(User));
            entity.GameMaster = user.Id;
            entity.GameUsers.Add(new GameUser(user.Id, user, entity.Id, entity));
            await _calendarService.InsertAsync(dto.GameTime!);
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();
            _sessionService.SetCurrentGameId(entity.Id);
            return Mapper.Map<Game, Dto.Game>(entity);
        }

        public override Task<Dto.Game?> UpdateAsync(int id, Dto.GameInput entity)
        {
            return base.UpdateAsync(id, entity);
        }

        public override Task<bool> DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<Dto.Game?> AddNewGame(int gameId)
        {
            var game = await GetGameById(gameId);
            var userId = _sessionService.GetCurrentUserId();
            User user = await _userService.GetUserById(userId) ?? throw new IllegalStateException(nameof(User));
            if (!game.IsInGame(userId))
            {
                game.GameUsers.Add(new GameUser(user.Id, user, game.Id, game));
                await DbContext.SaveChangesAsync();
            }

            _sessionService.SetCurrentGameId(game.Id);
            return Mapper.Map<Game, Dto.Game>(game);
        }

        

        public async Task<Game> GetGameById(int gameId)
            => await Query.Include(g => g.GameUsers)
                .Include(g => g.Items)
                .Include(g => g.Events)
                .Include(g => g.Notes)
                .Include(g => g.Notifications)
                .Include(g => g.GameTime)
                .FirstOrDefaultAsync(x => x.Id == gameId);
    }
}
