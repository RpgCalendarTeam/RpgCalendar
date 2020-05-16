namespace RPGCalendar.Core.Dto
{
    using System;

    public abstract class UserInfo
    {
        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set => _username = value ?? throw new ArgumentNullException(nameof(Username));
        }

        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set => _email = value ?? throw new ArgumentNullException(nameof(Email));
        }
    }

    public class UserInput : UserInfo
    {
        
        public string? AuthId { get; set; }
    }

    public class User : UserInfo, IEntity
    {
        public int Id { get; set; }

    }
}
