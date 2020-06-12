namespace RPGCalendar.Core.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Month
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int LengthInDays { get; set; }
    }
}
