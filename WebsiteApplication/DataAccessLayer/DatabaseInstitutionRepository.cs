using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;

namespace WebsiteApplication.DataAccessLayer
{
    class DatabaseInstitutionRepository : IInstitutionRepository
    {
        private readonly WebsiteDatabaseContext _context;
        public IQueryable<Institution> Institutions { get; }

        public DatabaseInstitutionRepository(WebsiteDatabaseContext context)
        {
            _context = context;
        }

        public void Add(Institution institution)
        {
            _context.Institutions.Add(institution);
        }

        public void Update(Institution institution)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
