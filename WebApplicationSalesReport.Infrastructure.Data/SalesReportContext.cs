using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplicationSalesReport.Domain.Core;

namespace WebApplicationSalesReport.Infrastructure.Data
{
    public class SalesReportContext : DbContext
    {
        public SalesReportContext()
        {
        }
        public SalesReportContext( DbContextOptions< SalesReportContext > options ) : base( options )
        {
            Database.EnsureCreated();
        }

        public virtual DbSet< Client > Clients { get; set; }
        public virtual DbSet< Purchase > Purchases { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity< Client >( e => e.HasKey( m => m.ClientId ) );
            modelBuilder.Entity< Purchase >();
        }

        internal static List< Client > GetTestDataClientsWithPurchases()
        {
            var piter = new Client { Name = "Peter", Purchases = new List< Purchase >() };
            var nancy = new Client { Name = "Nancy", Purchases = new List< Purchase >() };
            var met = new Client { Name = "Met", Purchases = new List< Purchase >() };
            var ivan = new Client { Name = "Ivan", Purchases = new List< Purchase >() };
            var purchase1 = new Purchase { Name = "Car", Date = Convert.ToDateTime( "01.01.2021" ), ClientId = piter.ClientId };
            var purchase2 = new Purchase { Name = "Table", Date = Convert.ToDateTime( "20.03.2021" ), ClientId = piter.ClientId };
            var purchase3 = new Purchase { Name = "Airplane", Date = Convert.ToDateTime( "11.12.2020" ), ClientId = nancy.ClientId };
            var purchase4 = new Purchase { Name = "Yacht", Date = Convert.ToDateTime( "06.06.2020" ), ClientId = nancy.ClientId };
            var purchase5 = new Purchase { Name = "PC", Date = Convert.ToDateTime( "15.05.2021" ), ClientId = met.ClientId };
            var purchase6 = new Purchase { Name = "House", Date = Convert.ToDateTime( "01.01.2021" ), ClientId = met.ClientId };
            var purchase7 = new Purchase { Name = "Shoes", Date = Convert.ToDateTime( "02.02.2021" ), ClientId = ivan.ClientId };
            var purchase8 = new Purchase { Name = "Clothes", Date = Convert.ToDateTime( "03.03.2021" ), ClientId = ivan.ClientId };
            piter.Purchases.AddRange( new List< Purchase > { purchase1, purchase2 } );
            nancy.Purchases.AddRange( new List< Purchase > { purchase3, purchase4 } );
            met.Purchases.AddRange( new List< Purchase > { purchase5, purchase6 } );
            ivan.Purchases.AddRange( new List< Purchase > { purchase7, purchase8 } );

            return new List< Client > { piter, nancy, met, ivan };
        }
    }
}