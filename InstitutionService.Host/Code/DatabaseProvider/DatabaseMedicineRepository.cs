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
    class DatabaseMedicineRepository : IMedicineRepository
    {
        private readonly InstitutionServiceDatabaseContext _context;
        public IQueryable<Medicine> Medicines => _context.Medicines;

        public DatabaseMedicineRepository(IRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

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
