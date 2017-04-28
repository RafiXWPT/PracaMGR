using System;
using System.Linq;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class DatabaseHospitalizationRepository : IHospitalizationRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabaseHospitalizationRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<Hospitalization> Hospitalizations => _context.Hospitalizations;

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