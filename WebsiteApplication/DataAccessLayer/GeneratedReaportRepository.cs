using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;

namespace WebsiteApplication.DataAccessLayer
{
    public class GeneratedReaportRepository : IRepository<GeneratedReport>
    {
        private readonly WebsiteDatabaseContext _context;

        public GeneratedReaportRepository(WebsiteDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<GeneratedReport> Entities => _context.GeneratedReaports;

        public int CreatedInLast(DateTime time, string username = null)
        {
            return username == null
                ? Entities.Count(e => e.CreatedAt > time)
                : Entities.Count(e => e.CreatedAt > time && e.ReportRequest.CreatedBy == username);
        }

        public int CreatedBetween(DateTime from, DateTime to, string username = null)
        {
            return username == null
                ? Entities.Count(e => e.CreatedAt > from && e.CreatedAt < to)
                : Entities.Count(e => e.CreatedAt > from && e.CreatedAt < to && e.ReportRequest.CreatedBy == username);
        }

        public void Create(GeneratedReport entity)
        {
            entity.GeneratedReportId = Guid.NewGuid();
            _context.GeneratedReaports.Add(entity);
            SaveChanges();
        }

        public GeneratedReport Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.GeneratedReportId == entityId);
        }

        public void Update(GeneratedReport entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(GeneratedReport entity)
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