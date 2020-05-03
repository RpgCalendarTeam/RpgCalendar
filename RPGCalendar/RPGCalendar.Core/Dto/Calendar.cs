namespace RPGCalendar.Core.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class CalendarInput
    {
        [Required]
        public long Time { get; set; }
        [Required]
        public int MonthsInYear { get; set; }
        [Required]
        public int WeeksInMonth { get; set; }
        [Required]
        public int DaysInWeek { get; set; }
        public string[]? Months { get; set; }
        public string[]? Days { get; set; }

    }
    public class Calendar : CalendarInput
    {
        public int Id { get; set; }
        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentWeek { get; set; }
        public int CurrentDay { get; set; }
        public int CurrentSecond { get; set; }
    }
}
