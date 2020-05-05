using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCalendar.Core.Exceptions
{
    public class UserPermissionException : Exception
    {
        public UserPermissionException(string message) 
        :base(message)
        { }
    }
}
