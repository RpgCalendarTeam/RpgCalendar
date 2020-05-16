namespace RPGCalendar.Data.GameCalendar
{
    public class Calendar : FingerPrintEntityBase
    {
        public const int DayInSec = 86400; //constant one day in seconds
        public long Time { get; set; } //current time of the game in seconds.
        public int MonthsInYear { get; set; }
        public int WeeksInMonth { get; set; }
        public int DaysInWeek { get; set; }
        public int CurrentYear 
        {
            get
            {
                long y = Time / (DayInSec * DaysInWeek * WeeksInMonth * MonthsInYear);
                return (int)y;
            }
        }
        public int CurrentMonth
        {
            get
            {
                long m = (Time % (DayInSec * DaysInWeek * WeeksInMonth * MonthsInYear)) / (DayInSec * DaysInWeek * WeeksInMonth);
                return (int)m;
            }
        }
        public int CurrentWeek
        {
            get
            {
                long w = (Time % (DayInSec * DaysInWeek * WeeksInMonth)) / (DayInSec * DaysInWeek);
                return (int)w;
            }
        }
        public int CurrentDay
        {
            get
            {
                long d = (Time % (DayInSec * DaysInWeek)) / (DayInSec);
                return (int)d;
            }
        }
        public int CurrentSecond
        {
            get
            {
                long s = Time % DayInSec;
                return (int)s;
            }
        }
        public string[]? Months { get; set; } //fictional names for months.
        public string[]? Days { get; set; } //fictional names for days.

        public void addTime(long time) => this.Time += time;

    }
}
