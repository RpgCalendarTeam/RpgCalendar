namespace RPGCalendar.Data.GameObjects
{
    using System.Collections.Generic;

    public abstract class GameObject : FingerPrintEntityBase
    {
#nullable disable
        public int GameId { get; set; }
        public Game Game { get; set; }
#nullable enable
    }
}
