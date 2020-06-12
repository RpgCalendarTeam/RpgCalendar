namespace RPGCalendar.Core.Dto
{
    using System.ComponentModel.DataAnnotations;

    public abstract class GameBase
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? GameSystem { get; set; }
    }
    public class GameInput : GameBase
    {
        
        [Required]
        public CalendarInput? Calendar { get; set; }
    }
    public class Game : GameBase, IEntity
    {
        public int Id { get; set; }
        public int GameMaster { get; set; }
        public Calendar? Calendar { get; set; }
    }
}
