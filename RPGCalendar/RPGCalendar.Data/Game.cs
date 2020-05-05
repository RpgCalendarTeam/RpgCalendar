namespace RPGCalendar.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using GameObjects;
    using Joins;
    using GameCalendar;

    public class Game : FingerPrintEntityBase
    {
        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentNullException(nameof(Title));
        }
        private string _title = string.Empty;

        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(Description));
        }
        private string _description = string.Empty;

        public string GameSystem
        {
            get => _gameSystem;
            set => _gameSystem = value ?? throw new ArgumentNullException(nameof(GameSystem));
        }
        private string _gameSystem = string.Empty;
        public int GameMaster { get; set; }
        public Calendar? GameTime { get; set; }

        //List of game items for game instance
        public ICollection<GameUser> GameUsers { get; set; } = new HashSet<GameUser>();
        public ICollection<Note> Notes { get; set; } = new HashSet<Note>();
        public ICollection<Event> Events { get; set; } = new HashSet<Event>();
        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();

        public bool IsInGame(int userId)
            => GameUsers.Any(e => e.UserId == userId);
    }
}
