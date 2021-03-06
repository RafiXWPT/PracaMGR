﻿using System;
using System.Linq;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> : IDbRepository
    {
        IQueryable<TEntity> Entities { get; }
        void Create(TEntity entity);
        TEntity Read(Guid entityId);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
    }

    public interface IDateTimeCountableRepository<TEntity> : IRepository<TEntity>, IDateTimeCountable
    {

    }
}