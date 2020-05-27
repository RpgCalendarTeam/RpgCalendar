namespace RPGCalendar.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Exceptions;
    using Data.Joins;
    using Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Services;

    public interface IGameRepository : IEntityRepository<Game>
    {
        Task<Game?> AddNewGame(int gameId, User user);
        Task<Game?> AddNewGame(int gameId, User user, string playerClass, string playerBio);
    }
    public class GameRepository : EntityRepository<Game>, IGameRepository
    {
        private readonly ISessionService _sessionService;
        private readonly IUserRepository _userRepository;
        private readonly ICalendarRepository _calendarRepository;

        public GameRepository(ISessionService sessionService, ApplicationDbContext dbContext, IUserRepository userRepository, ICalendarRepository calendarRepository)
            : base(dbContext)
        {
            _sessionService = sessionService;
            _userRepository = userRepository;
            _calendarRepository = calendarRepository;
        }

        public override async Task<Game?> FetchByIdAsync(int id)
            => (await FetchAllAsync())
                .First(x => x.Id == id);

        public override async Task<List<Game>> FetchAllAsync() =>
            await Query.Include(g => g.GameUsers)
                .Include(g => g.Items)
                .Include(g => g.Events)
                .Include(g => g.Notes)
                .Include(g => g.Notifications)
                .Include(g => g.GameTime)
                .ToListAsync();

        public async Task<Game?> AddNewGame(int gameId, User user)
        {
            var game = await FetchByIdAsync(gameId);
            if (game is null || game.IsInGame(user.Id))
                return game;
            game.GameUsers.Add(new GameUser(user.Id, user, game.Id, game));
            await DbContext.SaveChangesAsync();
            return game;
        }

        public async Task<Game?> AddNewGame(int gameId, User user, string playerClass, string playerBio)
        {
            var game = await FetchByIdAsync(gameId);
            if (game is null || game.IsInGame(user.Id))
                return game;
            game.GameUsers.Add(new GameUser(user.Id, user, game.Id, game, playerClass, playerBio));
            await DbContext.SaveChangesAsync();
            return game;
        }


    }
}
