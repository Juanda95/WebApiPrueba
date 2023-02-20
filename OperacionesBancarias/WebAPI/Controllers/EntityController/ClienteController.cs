using Application.Feauties.Clientes.Commands.CreateClienteCommand;
using Application.Feauties.Clientes.Commands.DeleteClienteCommand;
using Application.Feauties.Clientes.Commands.UpdateClienteCommand;
using Application.Feauties.Clientes.Queries.GetallClientes;
using Application.Feauties.Clientes.Queries.GetClienteById;
using Application.Feauties.Reportes.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Feauties.Reportes.Queries.GetReporteGeneralQuery;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.Controllers.v1
{
    
    public class ClienteController : BaseApiController
    {
        // GET
        [HttpGet]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetClienteAllQuery()));
        }

        // GET
        [HttpGet("{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClienteByIdQuery { Id = id }));
        }

        [HttpGet("/reportes/{id}/{FechaIni}/{FechaFin}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Get(int id, DateTime FechaIni, DateTime FechaFin)
        {
            return Ok(await Mediator.Send(new GetReporteGeneralQuery { ClienteId = id, FechaInicial = FechaIni, FechaFinal = FechaFin }));
        }


        //POST 
        [HttpPost]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT 
        [HttpPut("{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Put(int id, UpdateClienteCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }else
            {
                return Ok(await Mediator.Send(command));
            }
           
        }

        //DELETE Api/<Controller>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteclienteComand { Id = id } ));
        }
    }
}
