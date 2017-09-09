using System;
using System.Data.Entity;
using System.Linq;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class HospitalizationRepository : IRepository<Hospitalization>
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public HospitalizationRepository(IDbRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<Hospitalization> Entities => _context.Hospitalizations;
        public void Create(Hospitalization entity)
        {
            _context.Hospitalizations.Add(entity);
            SaveChanges();
        }

        public Hospitalization Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.HospitalizationId == entityId);
        }

        public void Update(Hospitalization entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Hospitalization entity)
        {
            _context.Hospitalizations.Remove(entity);
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