using Application.Feauties.Cuentas.Common.CreateCuentaCommand;
using Application.Feauties.Cuentas.Common.DeleteCuentaCommand;
using Application.Feauties.Cuentas.Common.UpdateCuentaCommand;
using Application.Feauties.Cuentas.Queries.GetallCuentas;
using Application.Feauties.Cuentas.Queries.GetClienteById;
using Application.Feauties.Movimientos.Commands.CreateMovimientoCommand;
using Application.Feauties.Movimientos.Commands.DeleteMovimientoCommand;
using Application.Feauties.Movimientos.Commands.UpdateMovimientoCommand;
using Application.Feauties.Movimientos.Queries.GetallMovimientos;
using Application.Feauties.Movimientos.Queries.GetMovimientoById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Entity_Controller
{

    public class MovimientoController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetMovimientosAllQuery()));
        }

        // GET
        [HttpGet("{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMovimientoByIdQuery { Id = id }));
        }
        //POST 
        [HttpPost]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Post(CreateMovimientoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        //DELETE Api/<Controller>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteMovimientoCommand { Id = id }));
        }

    }
}
