using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;

namespace WebsiteApplication.DataAccessLayer
{
    public class ReaportRequestRepository : IRepository<ReportRequest>
    {
        private readonly WebsiteDatabaseContext _context;

        public ReaportRequestRepository(WebsiteDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<ReportRequest> Entities => _context.ReaportRequests;

        public int CreatedInLast(DateTime time, string username = null)
        {
            return username == null
                ? Entities.Count(e => e.CreatedAt > time)
                : Entities.Count(e => e.CreatedAt > time && e.CreatedBy == username);
        }

        public int CreatedBetween(DateTime from, DateTime to, string username = null)
        {
            return username == null
                ? Entities.Count(e => e.CreatedAt > from && e.CreatedAt < to)
                : Entities.Count(e => e.CreatedAt > from && e.CreatedAt < to && e.CreatedBy == username);
        }

        public void Create(ReportRequest entity)
        {
            entity.ReportRequestId = Guid.NewGuid();
            _context.ReaportRequests.Add(entity);
            SaveChanges();
        }

        public ReportRequest Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.ReportRequestId == entityId);
        }

        public void Update(ReportRequest entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(ReportRequest entity)
        {
            _context.ReaportRequests.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}