using System;

namespace Domain.Interfaces
{
    public interface IDateTimeCountable
    {
        int CreatedInLast(DateTime from, string username = null);
        int CreatedBetween(DateTime from, DateTime to, string username = null);
    }
}