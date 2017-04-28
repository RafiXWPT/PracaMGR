using System;
using System.Linq;
using Domain.Interfaces;
using Domain.Inventory;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class DatabaseUsedMedicineRepository : IUsedMedicineRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabaseUsedMedicineRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<UsedMedicine> UsedMedicines => _context.UsedMedicines;

        public void Update(UsedMedicine usedMedicine)
        {
            throw new NotImplementedException();
        }

        public void Add(UsedMedicine usedMedicine)
        {
            _context.UsedMedicines.Add(usedMedicine);
            ;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}