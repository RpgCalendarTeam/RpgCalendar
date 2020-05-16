namespace RPGCalendar.Core.Services
{
    using Data;
    using Data.Exceptions;
    using Microsoft.AspNetCore.Http;

    public interface ISessionService
    {
        void SetCurrentUserId(int userId);
        int GetCurrentUserId();
        void ClearSessionUser();
        void SetCurrentGameId(int gameId);
        int GetCurrentGameId();
        void ClearSessionGame();
    }
    public class SessionService : ISessionService
    {
        private readonly ISession _session;
        private const string UserKey = "User";
        private const string GameKey = "Game";
        public SessionService(IHttpContextAccessor contextAccessor)
        {
            _session = contextAccessor.HttpContext.Session;
        }

        public void SetCurrentUserId(int user)
        {
            ClearSessionUser();
            _session.SetInt32(UserKey, user);
        }

        public int GetCurrentUserId()
            => _session.GetInt32(UserKey) ?? throw new IllegalStateException(nameof(User));

        public void ClearSessionUser()
        {
            _session.Remove(UserKey);
            ClearSessionGame();
        }

        public void SetCurrentGameId(int gameId)
        {
            ClearSessionGame();
            _session.SetInt32(GameKey, gameId);
        }

        public int GetCurrentGameId()
            => _session.GetInt32(GameKey) ?? throw new IllegalStateException(nameof(Game));

        public void ClearSessionGame()
            => _session.Remove(GameKey);
    }
}
