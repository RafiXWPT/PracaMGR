using System;
using System.Linq;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class DatabaseExaminationRepository : IExaminationRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabaseExaminationRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<Examination> Examinations => _context.Examinations;

        public void Update(Examination examination)
        {
            throw new NotImplementedException();
        }

        public void Add(Examination examination)
        {
            _context.Examinations.Add(examination);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}