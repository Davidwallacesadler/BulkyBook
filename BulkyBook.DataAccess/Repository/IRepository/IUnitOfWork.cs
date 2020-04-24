using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // This is a wrapper for the repository interfaces:
    public interface IUnitOfWork : IDisposable
    {
        IShoppingCartRepository ShoppingCart { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ICompanyRepository Company { get; }
        ICoverTypeRepository CoverType { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ISP_Call SP_Call { get; }

        void Save();

    }
}
