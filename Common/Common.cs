using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.Common
{
    public class Common
    {
        public static DateTime ConvertToUserTimezone(DateTime dateTime, string timezone)
        {
            TimeZoneInfo userTimezone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            DateTime userTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, userTimezone);

            return userTime;
        }
    }

    public enum USERROLES
    {
        WRITER,
        PUBLISHER,
        USER
    }
}
