using Application.Feauties.Clientes.Commands.DeleteClienteCommand;
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

namespace Application.Feauties.Cuentas.Common.DeleteCuentaCommand
{
    public  class DeleteCuentaComand : IRequest<Response<int>>
    {
        public int Id { get; set; }

    }
    public class DeleteCuentaCommandHandlers : IRequestHandler<DeleteCuentaComand, Response<int>>
    {
        private readonly IRepositoryAsync<Cuenta> _repositoryAsync;
  

        public DeleteCuentaCommandHandlers(IRepositoryAsync<Cuenta> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;

        }


        public async Task<Response<int>> Handle(DeleteCuentaComand request, CancellationToken cancellationToken)
        {
            var Cuenta = await _repositoryAsync.GetByIdAsync(request.Id);
            if (Cuenta == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id{request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(Cuenta);
                return new Response<int>(Cuenta.Id);
            }
        }

    }
}
