using System;

namespace WebApplicationSalesReport.Domain.Core
{
    public sealed class Purchase
    {
        public int PurchaseId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
    }
}