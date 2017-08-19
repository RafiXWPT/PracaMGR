using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Interfaces;

namespace WebsiteApplication.CodeBehind.Helpers
{
    public static class TimeHelper
    {
        public static DateTime LastHour => DateTime.Now.AddHours(-1);
        public static DateTime LastDay => DateTime.Now.AddDays(-1);
        public static DateTime LastWeek => DateTime.Now.AddDays(-7);
        public static DateTime LastMonth => DateTime.Now.AddMonths(-1);
        public static DateTime LastYear => DateTime.Now.AddYears(-1);

        public static bool IsCreatedCounterViolated(ICreatedCountable repository, string username)
        {
            if (repository.CreatedInLast(LastHour, username) > 10)
                return true;

            if (repository.CreatedInLast(LastDay) > 25)
                return true;

            if (repository.CreatedInLast(LastWeek) > 100)
                return true;

            if (repository.CreatedInLast(LastMonth) > 500)
                return true;

            if (repository.CreatedInLast(LastYear) > 6000)
                return true;

            return false;
        }
    }
}