using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    public class PatientRepository : Repository<InstitutionServiceDatabaseContext>, IRepository<Patient>
    {
        public IQueryable<Patient> Entities => Context.Patients;
        public void Create(Patient entity)
        {
            Context.Patients.Add(entity);
            SaveChanges();
        }

        public Patient Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.PatientId == entityId);
        }

        public void Update(Patient entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Patient entity)
        {
            Context.Patients.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public PatientRepository(IDbRepository context) : base(context)
        {
        }
    }
}