using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // This will be made public for accessing outside of this project
    // NOTE: this interface is generic - Hence the generic type T
    public interface IRepository<T> where T :class
    {
        // Here we will define all of the required CRUD operations:
        // Get by Id:
        T Get(int id);

        // Get All: ( NOTE: this method returns the IEnumerable Interface here, hence we are defining more default methods here such as filter, orderby...)
        IEnumerable<T> GetAll(
            // Get all / filter: (NOTE: syntax => Func<Input, Return>)
            Expression<Func<T, bool>> filter = null,
            // Order by:
            Func<IQueryable<T>, IQueryable<T>> orderBy = null,
            // Include properties used by eager loading:
            string includeProperties = null
            );

        // Get First or Defualt:
        T GetFirstOrDefault(
            // Get all / filter: (NOTE: syntax => Func<Input, Return>)
            Expression<Func<T, bool>> filter = null,
            // Include properties used by eager loading:
            string includeProperties = null
            );

        // Add:
        void Add(T entity);

        // Remove:
        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
