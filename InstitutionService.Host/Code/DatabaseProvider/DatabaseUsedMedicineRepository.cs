using System;
using System.Data.Entity;
using System.Linq;
using Domain.Interfaces;
using Domain.Inventory;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class DatabaseUsedMedicineRepository : IRepository<UsedMedicine>
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabaseUsedMedicineRepository(IDbRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }


        public IQueryable<UsedMedicine> Entities => _context.UsedMedicines;
        public void Create(UsedMedicine entity)
        {
            _context.UsedMedicines.Add(entity);
            SaveChanges();
        }

        public UsedMedicine Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.UsedMedicineId == entityId);
        }

        public void Update(UsedMedicine entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(UsedMedicine entity)
        {
            _context.UsedMedicines.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}