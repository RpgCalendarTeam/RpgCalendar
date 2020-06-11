/*
 * Source: https://code-maze.com/send-email-with-attachments-aspnetcore-2/
 * */
namespace RPGCalendar.Identity
{
    public class EmailConfiguration
    {
        public string? From { get; set; }
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
