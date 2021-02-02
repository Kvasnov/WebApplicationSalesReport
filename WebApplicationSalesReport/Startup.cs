using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationSalesReport.Domain.Core;
using WebApplicationSalesReport.Domain.Interfaces;
using WebApplicationSalesReport.Infrastructure.Data;

namespace WebApplicationSalesReport
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_2_2 );
            services.AddDbContext< SalesReportContext >( options => options.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ) ) );
            services.AddTransient< IClientRepository< Client >, ClientRepository >();
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if ( env.IsDevelopment() )
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}