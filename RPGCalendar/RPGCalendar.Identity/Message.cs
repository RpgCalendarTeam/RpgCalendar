/*
 * Source: https://code-maze.com/send-email-with-attachments-aspnetcore-2/
 * */

namespace RPGCalendar.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MimeKit;

    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }

    }
}
