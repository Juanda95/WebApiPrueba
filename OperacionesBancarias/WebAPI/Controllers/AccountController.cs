using Application.DTOs.Users;
using Application.Feauties.Usuarios.Commands.AuthenticateUsuario;
using Application.Feauties.Usuarios.Commands.RegisterCommands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateUsuarioCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddres = GenerateIpAddress()
            }));
        }

        private string GenerateIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                Nombre = request.Nombre,
                Apellido= request.Apellido,
                Email = request.Email,
                Password = request.Password,
                confirmPassword= request.confirmPassword,
                UserName = request.UserName,
                Origin = Request.Headers["origin"]
            }));
        }
    }
}
