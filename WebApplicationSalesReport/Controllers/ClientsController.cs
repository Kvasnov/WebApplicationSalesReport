using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplicationSalesReport.Domain.Core;
using WebApplicationSalesReport.Domain.Interfaces;

namespace WebApplicationSalesReport.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        public ClientsController( IClientRepository< Client > repository )
        {
            clientRepository = repository;
        }

        private readonly IClientRepository< Client > clientRepository;

        [HttpGet]
        public ActionResult< IEnumerable< Client > > GetClientList( string dateFrom, string dateTo, string name )
        {
            return new ActionResult< IEnumerable< Client > >( clientRepository.GetClientListWithFilters( dateFrom, dateTo, name ) );
        }

        [HttpGet( "{id}" )]
        public ActionResult< Client > GetClient( int id )
        {
            if ( id < 0 )
                return BadRequest();

            return clientRepository.GetClient( id );
        }

        [HttpPost]
        public void Post( [FromBody] Client client )
        {
            clientRepository.Create( client );
            clientRepository.Save();
        }

        [HttpPut( "{id}" )]
        public void Put( int id, [FromBody] Client client )
        {
            clientRepository.Update( client, id );
            clientRepository.Save();
        }

        [HttpDelete( "{id}" )]
        public void Delete( int id )
        {
            clientRepository.Delete( id );
            clientRepository.Save();
        }
    }
}