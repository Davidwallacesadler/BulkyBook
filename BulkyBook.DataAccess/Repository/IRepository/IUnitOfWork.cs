using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // This is a wrapper for the repository interfaces:
    public interface IUnitOfWork : IDisposable
    {
        ICoverTypeRepository CoverType { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ISP_Call SP_Call { get; }

        void Save();

    }
}
