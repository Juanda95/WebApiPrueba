using Application.Feauties.Clientes.Commands.CreateClienteCommand;
using Application.Feauties.Clientes.Commands.DeleteClienteCommand;
using Application.Feauties.Clientes.Commands.UpdateClienteCommand;
using Application.Feauties.Cuentas.Common.CreateCuentaCommand;
using Application.Feauties.Cuentas.Common.DeleteCuentaCommand;
using Application.Feauties.Cuentas.Common.UpdateCuentaCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Entity_Controller
{  
    public class CuentaController : BaseApiController
    {
        //POST 
        [HttpPost]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Post(CreateCuentaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT 
        [HttpPut("{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Put(int id, UpdateCuentaCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            else
            {
                return Ok(await Mediator.Send(command));
            }

        }

        //DELETE Api/<Controller>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCuentaComand { Id = id }));
        }
    }

   
}
