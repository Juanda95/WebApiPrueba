using Application.Feauties.Cuentas.Common.CreateCuentaCommand;
using Application.Feauties.Cuentas.Common.DeleteCuentaCommand;
using Application.Feauties.Cuentas.Common.UpdateCuentaCommand;
using Application.Feauties.Movimientos.Commands.CreateMovimientoCommand;
using Application.Feauties.Movimientos.Commands.DeleteMovimientoCommand;
using Application.Feauties.Movimientos.Commands.UpdateMovimientoCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Entity_Controller
{

    public class MovimientoController : BaseApiController
    {
        //POST 
        [HttpPost]
        //[Authorize(Roles = "Basic")]
        public async Task<IActionResult> Post(CreateMovimientoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        //DELETE Api/<Controller>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteMovimientoCommand { Id = id }));
        }

    }
}
