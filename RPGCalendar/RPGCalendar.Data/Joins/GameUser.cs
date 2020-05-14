using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCalendar.Data.Joins
{
    public class GameUser : FingerPrintEntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }


        public GameUser(int userid, User user, int gameId, Game game)
        {
            UserId = userid;
            User = user;
            GameId = gameId;
            Game = game;
        }

#nullable disable
        public GameUser()
        {

        }
#nullable enable
    }
    
}
