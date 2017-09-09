using System;
using System.Data.Entity;
using System.Linq;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class TreatmentRepository : IRepository<Treatment>
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public TreatmentRepository(IDbRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }


        public IQueryable<Treatment> Entities => _context.Treatments;

        public void Create(Treatment entity)
        {
            _context.Treatments.Add(entity);
            SaveChanges();
        }

        public Treatment Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.TreatmentId == entityId);
        }

        public void Update(Treatment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Treatment entity)
        {
            _context.Treatments.Remove(entity);
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