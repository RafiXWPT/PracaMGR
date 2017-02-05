using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public IQueryable<Institution> Institutions => _context.Institutions;

        public DatabaseInstitutionRepository(WebsiteDatabaseContext context)
        {
            _context = context;
        }

        public void Add(Institution institution)
        {
            _context.Institutions.Add(institution);
            _context.SaveChanges();
        }

        public void Update(Institution institution)
        {
            _context.Entry(institution).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Institution institution)
        {
            _context.Institutions.Remove(institution);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
