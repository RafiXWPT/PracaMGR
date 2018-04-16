using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Inventory;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class MedicineRepository : Repository<InstitutionServiceDatabaseContext>, IRepository<Medicine>
    {
        public IQueryable<Medicine> Entities => Context.Medicines;
        public void Create(Medicine entity)
        {
            Context.Medicines.Add(entity);
            SaveChanges();
        }

        public Medicine Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.MedicineId == entityId);
        }

        public void Update(Medicine entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Medicine entity)
        {
            Context.Medicines.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public int CreatedInLast(DateTime time, string username = null)
        {
            throw new NotImplementedException();
        }

        public int CreatedBetween(DateTime from, DateTime to, string username = null)
        {
            throw new NotImplementedException();
        }

        public MedicineRepository(IDbRepository context) : base(context)
        {
        }
    }
}