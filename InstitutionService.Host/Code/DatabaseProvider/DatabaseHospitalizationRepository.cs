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
    class DatabaseHospitalizationRepository : IHospitalizationRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;
        public IQueryable<Hospitalization> Hospitalizations => _context.Hospitalizations;

        public DatabaseHospitalizationRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }
        public void Update(Hospitalization hospitalization)
        {
            throw new NotImplementedException();
        }

        public void Add(Hospitalization hospitalization)
        {
            _context.Hospitalizations.Add(hospitalization);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
