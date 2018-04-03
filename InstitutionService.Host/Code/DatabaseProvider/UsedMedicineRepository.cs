using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Inventory;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class UsedMedicineRepository : Repository<InstitutionServiceDatabaseContext>, IRepository<UsedMedicine>
    {
        public IQueryable<UsedMedicine> Entities => Context.UsedMedicines;
        public void Create(UsedMedicine entity)
        {
            Context.UsedMedicines.Add(entity);
            SaveChanges();
        }

        public UsedMedicine Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.UsedMedicineId == entityId);
        }

        public void Update(UsedMedicine entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(UsedMedicine entity)
        {
            Context.UsedMedicines.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public UsedMedicineRepository(IDbRepository context) : base(context)
        {
        }
    }
}