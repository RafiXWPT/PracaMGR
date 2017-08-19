using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    public class DatabasePatientRepository : IRepository<Patient>
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabasePatientRepository(IDbRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }


        public IQueryable<Patient> Entities => _context.Patients;
        public void Create(Patient entity)
        {
            _context.Patients.Add(entity);
            SaveChanges();
        }

        public Patient Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.PatientId == entityId);
        }

        public void Update(Patient entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Patient entity)
        {
            _context.Patients.Remove(entity);
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
    }
}