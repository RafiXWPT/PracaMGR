using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class HospitalizationRepository : Repository<InstitutionServiceDatabaseContext>, IRepository<Hospitalization>
    {
        public IQueryable<Hospitalization> Entities => Context.Hospitalizations;
        public void Create(Hospitalization entity)
        {
            Context.Hospitalizations.Add(entity);
            SaveChanges();
        }

        public Hospitalization Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.HospitalizationId == entityId);
        }

        public void Update(Hospitalization entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Hospitalization entity)
        {
            Context.Hospitalizations.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public int CreatedInLast(DateTime time, string username = null)
        {
            throw new NotImplementedException();
        }

        public int CreatedBetween(DateTime from, DateTime to, string username = null)
        {
            throw new NotImplementedException();
        }

        public HospitalizationRepository(IDbRepository context) : base(context)
        {
        }
    }
}