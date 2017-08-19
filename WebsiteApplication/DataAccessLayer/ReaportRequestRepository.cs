﻿using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Domain.Interfaces;

namespace WebsiteApplication.DataAccessLayer
{
    public class ReaportRequestRepository : IRepository<ReaportRequest>
    {
        private readonly WebsiteDatabaseContext _context;

        public ReaportRequestRepository(WebsiteDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<ReaportRequest> Entities => _context.ReaportRequests;

        public int CreatedInLast(DateTime time, string username = null)
        {
            return username == null ? Entities.Count(e => e.CreatedAt > time) : Entities.Count(e => e.CreatedAt > time && e.CreatedBy == username);
        }

        public void Create(ReaportRequest entity)
        {
            entity.ReaportRequestId = Guid.NewGuid();
            _context.ReaportRequests.Add(entity);
            SaveChanges();
        }

        public ReaportRequest Read(Guid entityId)
        {
            return Entities.FirstOrDefault(e => e.ReaportRequestId == entityId);
        }

        public void Update(ReaportRequest entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(ReaportRequest entity)
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