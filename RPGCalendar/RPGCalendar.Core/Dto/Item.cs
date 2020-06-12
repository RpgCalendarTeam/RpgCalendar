namespace RPGCalendar.Core.Dto
{
    using System.ComponentModel.DataAnnotations;
    public class ItemInput
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Quality { get; set; }
        [Required]
        public decimal QuantityDenigration { get; set; }
        [Required]
        public decimal QualityDenigration { get; set; }
    }

    public class Item : ItemInput, IEntity
    {
        public int Id { get; set; }
    }
}
