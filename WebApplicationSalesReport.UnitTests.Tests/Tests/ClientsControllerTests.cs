using System.Linq;
using Moq;
using WebApplicationSalesReport.Controllers;
using WebApplicationSalesReport.Domain.Core;
using WebApplicationSalesReport.Domain.Interfaces;
using WebApplicationSalesReport.UnitTests.Tests.Common;
using Xunit;

namespace WebApplicationSalesReport.UnitTests.Tests.Tests
{
    public sealed class ClientsControllerTests
    {
        [Fact]
        public void GetClientList()
        {
            var mock = new Mock< IClientRepository< Client > >();
            mock.Setup( repo => repo.GetClientListWithFilters( It.IsAny< string >(), It.IsAny< string >(), It.IsAny< string >() ) ).Returns( ClientsDataForTest.GetTestClientsWithPurchases() );
            var clientController = new ClientsController( mock.Object );
            var result = clientController.GetClientList( null, null, null );
            Assert.Equal( 4, result.Value.ToList().Count );
            Assert.NotNull( result.Value );
        }
    }
}