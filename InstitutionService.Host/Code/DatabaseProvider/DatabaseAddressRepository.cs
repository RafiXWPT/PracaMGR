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
    class DatabaseAddressRepository : IAddressRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;
        public IQueryable<Address> Addresses => _context.Addresses;

        public DatabaseAddressRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public void Update(Address address)
        {
            throw new NotImplementedException();
        }

        public void Add(Address address)
        {
            _context.Addresses.Add(address);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
