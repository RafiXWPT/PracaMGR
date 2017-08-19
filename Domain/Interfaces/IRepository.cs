using System;
using System.Linq;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> : IDbRepository, ICreatedCountable
    {
        IQueryable<TEntity> Entities { get; }
        void Create(TEntity entity);
        TEntity Read(Guid entityId);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
    }
}