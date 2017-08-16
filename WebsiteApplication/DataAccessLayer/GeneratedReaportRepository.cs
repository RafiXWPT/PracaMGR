using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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
        public void Create(GeneratedReaport entity)
        {
            _context.GeneratedReaports.Add(entity);
            SaveChanges();
        }

        public GeneratedReaport Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.Id == entityId);
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