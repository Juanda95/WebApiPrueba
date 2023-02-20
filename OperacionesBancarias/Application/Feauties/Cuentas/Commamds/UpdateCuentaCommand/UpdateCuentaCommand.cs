using Application.Feauties.Clientes.Commands.UpdateClienteCommand;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Cuentas.Common.UpdateCuentaCommand
{
    public class UpdateCuentaCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public bool? Estado { get; set; }
    }
    public class UpdateCuentaCommandHandlers : IRequestHandler<UpdateCuentaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cuenta> _repositoryAsync;

        public UpdateCuentaCommandHandlers(IRepositoryAsync<Cuenta> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateCuentaCommand request, CancellationToken cancellationToken)
        {
            var Cuenta = await _repositoryAsync.GetByIdAsync(request.Id);
            if (Cuenta == null)
            {

                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                Cuenta.NumeroCuenta = request.NumeroCuenta;
                Cuenta.TipoCuenta = request.TipoCuenta;
                Cuenta.Estado = request.Estado;             

                await _repositoryAsync.UpdateAsync(Cuenta);
                return new Response<int>(Cuenta.Id);
            }
        }
    }
}
