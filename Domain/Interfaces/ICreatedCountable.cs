using System;

namespace Domain.Interfaces
{
    public interface ICreatedCountable
    {
        int CreatedInLast(DateTime time, string username = null);
    }
}