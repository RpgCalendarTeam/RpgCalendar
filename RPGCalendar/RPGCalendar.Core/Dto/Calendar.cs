namespace RPGCalendar.Core.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CalendarInput : IValidatableObject
    {
        [Required]
        [Range(1, long.MaxValue)]
        public long? HourLength { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long? DayLength { get; set; }
        [Required]
        public Month[]? Months { get; set; }
        [Required]
        public string[]? DaysOfWeek { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.HourLength! > this.DayLength)
                yield return new ValidationResult("Hour length must be less than Day Length");
            if(this.Months!.Length < 1)
                yield return new ValidationResult("Months must contain at least one month");
            if(this.DaysOfWeek!.Length<1)
                yield return new ValidationResult("Days of week must contain at least one value");

        }
    }
    public class Calendar : IEntity
    {
        public int Id { get; }
        public string? FormattedDate { get; set; }
        public int? FullYear { get; set; }
        public int? Month { get; set; }
        public int? Date { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
        public int? Second { get; set; }
        public string? Day { get; set; }
        public string? MonthName { get; set; }
        public int? YearLengthInMonths { get; set; }
        public int? MonthLengthInDays { get; set; }
        public int? WeekLengthInDays { get; set; }
        public int? DayLengthInHours { get; set;}
        public long? CurrentTimeInSeconds { get; set; }
        public string[]? MonthNames { get; set; }
        public string[]? DayNames { get; set; }

        
    }
}
