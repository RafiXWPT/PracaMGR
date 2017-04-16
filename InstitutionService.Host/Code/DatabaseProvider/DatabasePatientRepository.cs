using System;
using System.Linq;
using Domain;
using Domain.Interfaces;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    public class DatabasePatientRepository : IPatientRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabasePatientRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<Patient> Patients => _context.Patients;

        public void Update(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}