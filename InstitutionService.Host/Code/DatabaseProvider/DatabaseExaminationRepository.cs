using System;
using System.Data.Entity;
using System.Linq;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class DatabaseExaminationRepository : IRepository<Examination>
    {
        private readonly InstitutionServiceDatabaseContext _context;

        public DatabaseExaminationRepository(IDbRepository context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public IQueryable<Examination> Entities => _context.Examinations;

        public void Create(Examination entity)
        {
            _context.Examinations.Add(entity);
            SaveChanges();
        }

        public Examination Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.ExaminationId == entityId);
        }

        public void Update(Examination entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Examination entity)
        {
            _context.Examinations.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}