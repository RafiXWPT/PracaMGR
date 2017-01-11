using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    public class DatabaseRepository : IPatientRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public IQueryable<Patient> Patients => _context.Patients;

        public DatabaseRepository(IDatabaseContext context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }
        public void Update(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
