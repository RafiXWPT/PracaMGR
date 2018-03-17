﻿using System;
using System.Data.Entity;
using System.Linq;
using Domain.Interfaces;
using Domain.Inventory;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class MedicineRepository : IRepository<Medicine>
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public MedicineRepository(IDbRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<Medicine> Entities => _context.Medicines;
        public void Create(Medicine entity)
        {
            _context.Medicines.Add(entity);
            SaveChanges();
        }

        public Medicine Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.MedicineId == entityId);
        }

        public void Update(Medicine entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Medicine entity)
        {
            _context.Medicines.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int CreatedInLast(DateTime time, string username = null)
        {
            throw new NotImplementedException();
        }

        public int CreatedBetween(DateTime from, DateTime to, string username = null)
        {
            throw new NotImplementedException();
        }
    }
}