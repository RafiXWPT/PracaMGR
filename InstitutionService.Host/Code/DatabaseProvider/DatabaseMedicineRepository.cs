using System;
using System.Linq;
using Domain.Interfaces;
using Domain.Inventory;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class DatabaseMedicineRepository : IMedicineRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabaseMedicineRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<Medicine> Medicines => _context.Medicines;

        public void Update(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public void Add(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}