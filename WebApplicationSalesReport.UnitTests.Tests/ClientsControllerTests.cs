using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using WebApplicationSalesReport.Controllers;
using WebApplicationSalesReport.Domain.Core;
using WebApplicationSalesReport.Domain.Interfaces;
using Xunit;

namespace WebApplicationSalesReport.UnitTests.Tests
{
    public sealed class ClientsControllerTests
    {
        [Fact]
        public void GetClientListWithFilters()
        {
            const string dateTimeFrom = "01.01.2021";
            const string dateTimeTo = "01.01.2021";
            const string clientName = "Peter";
            var mock = new Mock< IClientRepository< Client > >();
            mock.Setup( repo => repo.GetClientListWithFilters( It.IsAny< string >(), It.IsAny< string >(), It.IsAny< string >() ) ).Returns< string, string, string >( GetTestClientsWithPurchases );
            var clientController = new ClientsController( mock.Object );
            var result = clientController.GetClientList( dateTimeFrom, dateTimeTo, null );
            Assert.Equal( 2, result.Value.ToList().Count );
            Assert.NotNull( result.Value );
            result = clientController.GetClientList( null, null, clientName );
            Assert.Equal( 1, result.Value.ToList().Count );
            Assert.NotNull( result.Value );
            result = clientController.GetClientList( dateTimeFrom, dateTimeTo, clientName );
            Assert.Equal( 1, result.Value.ToList().Count );
            Assert.NotNull( result.Value );
            result = clientController.GetClientList( null, null, null );
            Assert.Equal( 4, result.Value.ToList().Count );
            Assert.NotNull( result.Value );
        }

        private static List< Client > GetTestClientsWithPurchases( string dateFrom = null, string dateTo = null, string name = null )
        {
            var piter = new Client { Name = "Peter", ClientId = 1, Purchases = new List< Purchase >() };
            var nancy = new Client { Name = "Nancy", ClientId = 2, Purchases = new List< Purchase >() };
            var met = new Client { Name = "Met", ClientId = 3, Purchases = new List< Purchase >() };
            var ivan = new Client { Name = "Ivan", ClientId = 4, Purchases = new List< Purchase >() };
            var purchase1 = new Purchase { Name = "Car", Date = Convert.ToDateTime( "01.01.2021" ), PurchaseId = 1, ClientId = piter.ClientId };
            var purchase2 = new Purchase { Name = "Table", Date = Convert.ToDateTime( "20.03.2021" ), PurchaseId = 2, ClientId = piter.ClientId };
            var purchase3 = new Purchase { Name = "Airplane", Date = Convert.ToDateTime( "11.12.2020" ), PurchaseId = 3, ClientId = nancy.ClientId };
            var purchase4 = new Purchase { Name = "Yacht", Date = Convert.ToDateTime( "06.06.2020" ), PurchaseId = 4, ClientId = nancy.ClientId };
            var purchase5 = new Purchase { Name = "PC", Date = Convert.ToDateTime( "15.05.2021" ), PurchaseId = 5, ClientId = met.ClientId };
            var purchase6 = new Purchase { Name = "House", Date = Convert.ToDateTime( "01.01.2021" ), PurchaseId = 6, ClientId = met.ClientId };
            var purchase7 = new Purchase { Name = "Shoes", Date = Convert.ToDateTime( "02.02.2021" ), PurchaseId = 7, ClientId = ivan.ClientId };
            var purchase8 = new Purchase { Name = "Clothes", Date = Convert.ToDateTime( "03.03.2021" ), PurchaseId = 8, ClientId = ivan.ClientId };
            piter.Purchases.AddRange( new List< Purchase > { purchase1, purchase2 } );
            nancy.Purchases.AddRange( new List< Purchase > { purchase3, purchase4 } );
            met.Purchases.AddRange( new List< Purchase > { purchase5, purchase6 } );
            ivan.Purchases.AddRange( new List< Purchase > { purchase7, purchase8 } );
            var clients = new List< Client > { piter, nancy, met, ivan };
            if ( DateTime.TryParse( dateFrom, out var dateTimeFrom ) && DateTime.TryParse( dateTo, out var dateTimeTo ) )
                clients = clients.Select( item => new Client { Name = item.Name, Purchases = item.Purchases.Where( purchase => purchase.Date >= dateTimeFrom && purchase.Date <= dateTimeTo ).ToList() } ).
                                  Where( client => client.Purchases.Any() ).
                                  ToList();

            if ( !string.IsNullOrEmpty( name ) )
                clients = clients.Where( client => client.Name.Contains( name ) ).ToList();

            return clients;
        }
    }
}