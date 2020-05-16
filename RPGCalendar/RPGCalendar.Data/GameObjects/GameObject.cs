namespace RPGCalendar.Data.GameObjects
{
    public abstract class GameObject : FingerPrintEntityBase
    {
#nullable disable
        public int GameId { get; set; }
        public Game Game { get; set; }
#nullable enable
    }
}
