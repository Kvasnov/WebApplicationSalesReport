using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationSalesReport.Domain.Core;
using WebApplicationSalesReport.Domain.Interfaces;

namespace WebApplicationSalesReport.Infrastructure.Data
{
    public sealed class ClientRepository : IClientRepository< Client >
    {
        public ClientRepository( SalesReportContext db )
        {
            this.db = db;

            if ( !db.Clients.Any() )
            {
                this.db.Clients.AddRange( SalesReportContext.GetTestDataClientsWithPurchases() );
                this.db.SaveChanges();
            }
        }

        private readonly SalesReportContext db;

        public void Dispose()
        {
        }

        public IEnumerable< Client > GetClientListWithFilters( string dateFrom, string dateTo, string name )
        {
            IQueryable< Client > clients = db.Clients;
            if ( DateTime.TryParse( dateFrom, out var dateTimeFrom ) && DateTime.TryParse( dateTo, out var dateTimeTo ) )
                clients = clients.Select( item => new Client { Name = item.Name, Purchases = item.Purchases.Where( purchase => purchase.Date >= dateTimeFrom && purchase.Date <= dateTimeTo ).ToList() } ).
                                  Where( client => client.Purchases.Any() );

            if ( !string.IsNullOrEmpty( name ) )
                clients = clients.Where( client => client.Name.Contains( name ) );

            return clients.ToList();
        }

        public Client GetClient( int id )
        {
            return db.Clients.Find( id );
        }

        public void Create( Client item )
        {
            db.Clients.Add( item );
        }

        public void Update( Client item, int id )
        {
            var updatedClient = db.Clients.SingleOrDefault( client => client.ClientId == id );
            if ( updatedClient != null )
                updatedClient.Name = item.Name;
        }

        public void Delete( int id )
        {
            var removedClient = db.Clients.FirstOrDefault( client => client.ClientId == id );
            if ( removedClient != null )
                db.Clients.Remove( removedClient );
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}