using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    // This interface is for stored proceedure calls:
    public interface ISP_Call : IDisposable
    {
        // NOTE: installing dapper for dynamic parameters usage here:
        // Retreive a single record: (returns integer or boolean value)
        T Single<T>(string procedureName, DynamicParameters param = null);

        // Execute something to the db without retreiving anything:
        void Execute(string procedureName, DynamicParameters param = null);

        // Retrieve one complete row/record: (returns a complete row):
        T OneRecord<T>(string procedureName, DynamicParameters param = null);

        // Retreive all the rows:
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);

        // Stored proceedure that returns two tables
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);
    }
}
