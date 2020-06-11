﻿namespace RPGCalendar.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordModel
    {
        [Required]
        public string? Password { get; set; }

        [Required]
        public string? ConfirmPassword { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Token { get; set; }
    }
}
