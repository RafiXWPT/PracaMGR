using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;

namespace WebsiteApplication.DataAccessLayer
{
    public class GeneratedReaportRepository : IRepository<GeneratedReaport>
    {
        private readonly WebsiteDatabaseContext _context;

        public GeneratedReaportRepository(WebsiteDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<GeneratedReaport> Entities => _context.GeneratedReaports;

        public int CreatedInLast(DateTime time, string username = null)
        {
            return username == null ? Entities.Count(e => e.CreatedAt > time) : Entities.Count(e => e.CreatedAt > time && e.ReaportRequest.CreatedBy == username);
        }

        public void Create(GeneratedReaport entity)
        {
            entity.GeneratedReaportId = Guid.NewGuid();
            _context.GeneratedReaports.Add(entity);
            SaveChanges();
        }

        public GeneratedReaport Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.GeneratedReaportId == entityId);
        }

        public void Update(GeneratedReaport entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(GeneratedReaport entity)
        {
            _context.GeneratedReaports.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}