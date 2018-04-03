using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DatabaseProvider
{
    internal class ExaminationRepository : Repository<InstitutionServiceDatabaseContext>, IRepository<Examination>
    {
        public IQueryable<Examination> Entities => Context.Examinations;

        public void Create(Examination entity)
        {
            Context.Examinations.Add(entity);
            SaveChanges();
        }

        public Examination Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.ExaminationId == entityId);
        }

        public void Update(Examination entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(Examination entity)
        {
            Context.Examinations.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public ExaminationRepository(IDbRepository context) : base(context)
        {
        }
    }
}