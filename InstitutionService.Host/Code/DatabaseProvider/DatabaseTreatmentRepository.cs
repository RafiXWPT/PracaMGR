using System;
using System.Linq;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class DatabaseTreatmentRepository : ITreatmentRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabaseTreatmentRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<Treatment> Treatments => _context.Treatments;

        public void Update(Treatment treatment)
        {
            throw new NotImplementedException();
        }

        public void Add(Treatment treatment)
        {
            _context.Treatments.Add(treatment);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}