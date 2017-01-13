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
    class DatabaseExaminationRepository : IExaminationRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;
        public IQueryable<Examination> Examinations => _context.Examinations;

        public DatabaseExaminationRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

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
