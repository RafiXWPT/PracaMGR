using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class TreatmentRepository : Repository<InstitutionServiceDatabaseContext>, IRepository<Treatment>
    {
        public IQueryable<Treatment> Entities => Context.Treatments;

        public void Create(Treatment entity)
        {
            Context.Treatments.Add(entity);
            SaveChanges();
        }

        public Treatment Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.TreatmentId == entityId);
        }

        public void Update(Treatment entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Treatment entity)
        {
            Context.Treatments.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public TreatmentRepository(IDbRepository context) : base(context)
        {
        }
    }
}