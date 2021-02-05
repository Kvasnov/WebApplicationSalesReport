using System.Collections;
using System.Linq;
using Moq;
using WebApplicationSalesReport.Infrastructure.Data;
using WebApplicationSalesReport.UnitTests.Tests.Common;
using Xunit;

namespace WebApplicationSalesReport.UnitTests.Tests.Tests
{
    public sealed class ClientRepositoryTests
    {
        private const string dateTimeFrom = "01.01.2021";
        private const string dateTimeTo = "01.01.2021";
        private const string clientName = "Peter";

        [Fact]
        public void GetClientListWithFilters()
        {
            var data = ClientsDataForTest.GetTestClientsWithPurchases().ToList();
            var mockContext = new Mock< SalesReportContext >();
            mockContext.Setup( c => c.Clients ).Returns( DbContextMock.GetQueryableMockDbSet( data ) );
            var repository = new ClientRepository( mockContext.Object );
            var result = repository.GetClientListWithFilters( null, null, null ).ToList();
            Assert.Equal( 4, result.Count );
            Assert.NotNull( result );
            result = repository.GetClientListWithFilters( dateTimeFrom, dateTimeTo, null ).ToList();
            Assert.Equal( 2, result.Count );
            Assert.NotNull( result );
            result = repository.GetClientListWithFilters( null, null, clientName ).ToList();
            Assert.Single( ( IEnumerable ) result );
            Assert.NotNull( result );
            result = repository.GetClientListWithFilters( dateTimeFrom, dateTimeTo, clientName ).ToList();
            Assert.Single( ( IEnumerable ) result );
            Assert.NotNull( result );
        }
    }
}