using System;
using System.Collections.Generic;

namespace WebApplicationSalesReport.Domain.Interfaces
{
    public interface IClientRepository< TEntity > : IDisposable
    {
        IEnumerable< TEntity > GetClientListWithFilters( string dateFrom, string dateTo, string name );
        TEntity GetClient( int id );
        void Create( TEntity item );
        void Update( TEntity item, int id );
        void Delete( int id );
        void Save();
    }
}