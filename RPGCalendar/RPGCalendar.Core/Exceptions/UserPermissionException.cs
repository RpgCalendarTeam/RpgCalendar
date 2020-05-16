using System;

namespace RPGCalendar.Core.Exceptions
{
    public class UserPermissionException : Exception
    {
        public UserPermissionException(string message) 
        :base(message)
        { }
    }
}
