namespace RPGCalendar.Core.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TimeChange : IValidatableObject
    {
        public long? SetTime { get; set; }
        public long? Seconds { get; set; }
        public int? Minutes { get; set; }
        public int? Hours { get; set; }
        public int? Days { get; set; }
        public int? Years { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(SetTime is null && Seconds is null && Minutes is null && Hours is null && Days is null && Years is null)
                yield return new ValidationResult("At least one value must be selected");
        }
    }
}
