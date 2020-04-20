using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        // Use dependency injection to inject the ApplicationDbContext into all of our repositories:
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            // Initialize repositories:
            // Category:
            Category = new CategoryRepository(_db);
            // CoverType:
            CoverType = new CoverTypeRepository(_db);
            // Stored Procedures:
            SP_Call = new SP_Call(_db);
        }

        // Initialize repository interfaces:
        // NOTE: this is a private set that is set in the constructor of the unit of work
        public ICoverTypeRepository CoverType { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        // NOTE: we need save inside our unit of work because we are not saving in any of our repositories.
        // Save() in unit of work is at the parent level!
        public void Save()
        {
            _db.SaveChanges();
        }

        // NOTE: none of this will be accessible in the main project unless registered within startup!
    }

}
