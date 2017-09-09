using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Interfaces;

namespace WebsiteApplication.CodeBehind.Helpers
{
    public static class TimeHelper
    {
        public static DateTime LastMinute => DateTime.Now.AddMinutes(-1);
        public static DateTime LastHour => DateTime.Now.AddHours(-1);
        public static DateTime LastDay => DateTime.Now.AddDays(-1);
        public static DateTime LastWeek => DateTime.Now.AddDays(-7);
        public static DateTime LastMonth => DateTime.Now.AddMonths(-1);
        public static DateTime LastYear => DateTime.Now.AddYears(-1);

        public static bool IsSearchCounterViolated(IDateTimeCountable repository, string username)
        {
            if (repository.CreatedInLast(LastMinute, username) > 1)
                return true;

            if (repository.CreatedInLast(LastHour, username) > 10)
                return true;

            if (repository.CreatedInLast(LastDay, username) > 25)
                return true;

            if (repository.CreatedInLast(LastWeek, username) > 100)
                return true;

            if (repository.CreatedInLast(LastMonth, username) > 500)
                return true;

            if (repository.CreatedInLast(LastYear, username) > 6000)
                return true;

            return false;
        }
    }
}