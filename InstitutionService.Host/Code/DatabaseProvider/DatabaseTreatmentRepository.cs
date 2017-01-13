using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    class DatabaseTreatmentRepository : ITreatmentRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;
        public IQueryable<Treatment> Treatments => _context.Treatments;

        public DatabaseTreatmentRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

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
