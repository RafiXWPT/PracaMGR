using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Inventory;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    class DatabaseUsedMedicineRepository : IUsedMedicineRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;
        public IQueryable<UsedMedicine> UsedMedicines => _context.UsedMedicines;

        public DatabaseUsedMedicineRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public void Update(UsedMedicine usedMedicine)
        {
            throw new NotImplementedException();
        }

        public void Add(UsedMedicine usedMedicine)
        {
            _context.UsedMedicines.Add(usedMedicine);
;        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
