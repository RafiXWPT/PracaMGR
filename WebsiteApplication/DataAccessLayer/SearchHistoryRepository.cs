using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Domain;
using Domain.Interfaces;

namespace WebsiteApplication.DataAccessLayer
{
    public class SearchHistoryRepository : IRepository<SearchHistory>
    {
        private readonly WebsiteDatabaseContext _context;

        public SearchHistoryRepository(WebsiteDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<SearchHistory> Entities => _context.SearchHistories;
        public void Create(SearchHistory entity)
        {
            _context.SearchHistories.Add(entity);
            SaveChanges();
        }

        public SearchHistory Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.SearchHistoryId == entityId);
        }

        public void Update(SearchHistory entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(SearchHistory entity)
        {
            _context.SearchHistories.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}