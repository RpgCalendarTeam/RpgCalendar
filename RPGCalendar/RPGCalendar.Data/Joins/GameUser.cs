namespace RPGCalendar.Data.Joins
{
    public class GameUser : FingerPrintEntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string? PlayerClass { get; set; }
        public string? PlayerBio { get; set; }

        public GameUser(int userid, User user, int gameId, Game game)
        {
            UserId = userid;
            User = user;
            GameId = gameId;
            Game = game;
        }

        public GameUser(int userid, User user, int gameId, Game game, string playerClass, string playerBio)
        {
            UserId = userid;
            User = user;
            GameId = gameId;
            Game = game;
            PlayerClass = playerClass;
            PlayerBio = playerBio;
        }

#nullable disable
        public GameUser()
        {

        }
#nullable enable
    }
    
}
