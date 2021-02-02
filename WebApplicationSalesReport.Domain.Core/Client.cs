using System.Collections.Generic;

namespace WebApplicationSalesReport.Domain.Core
{
    public sealed class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public List< Purchase > Purchases { get; set; }
    }
}