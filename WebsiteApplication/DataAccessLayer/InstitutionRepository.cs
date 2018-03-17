using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;

namespace WebsiteApplication.DataAccessLayer
{
    internal class InstitutionRepository : IRepository<Institution>
    {
        private readonly WebsiteDatabaseContext _context;

        public InstitutionRepository(WebsiteDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Institution> Entities => _context.Institutions;

        public void Create(Institution entity)
        {
            entity.InstitutionId = Guid.NewGuid();
            entity.Address.AddressId = Guid.NewGuid();
            _context.Institutions.Add(entity);
            SaveChanges();
        }

        public Institution Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.InstitutionId == entityId);
        }

        public void Update(Institution entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Institution entity)
        {
            _context.Institutions.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int CreatedInLast(DateTime time, string username = null)
        {
            throw new NotImplementedException();
        }

        public int CreatedBetween(DateTime from, DateTime to, string username = null)
        {
            throw new NotImplementedException();
        }
    }
}