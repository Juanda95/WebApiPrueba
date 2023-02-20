using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateEntityComand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class UpdatePersonaCommand : UpdateEntityComand
    {
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }

    public class UpdateClienteCommand : UpdatePersonaCommand
    {
        public string? Clienteid { get; set; }
        public string? Contrasena { get; set; }
        public bool? Estado { get; set; }
    }

    public class UpdateClienteCommandHandlers : IRequestHandler<UpdateClienteCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateClienteCommandHandlers(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<int>> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repositoryAsync.GetByIdAsync(request.Id);
            if(cliente == null)
            {

                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }else
            {
                cliente.Nombre = request.Nombre;
                cliente.Genero= request.Genero;
                cliente.Edad = request.Edad;
                cliente.Identificacion= request.Identificacion;
                cliente.Direccion= request.Direccion;
                cliente.Telefono= request.Telefono;
                cliente.Clienteid= request.Clienteid;
                cliente.Contrasena= request.Contrasena;
                cliente.Estado= request.Estado;

                await _repositoryAsync.UpdateAsync(cliente);
                return new Response<int>(cliente.Id); 
            }
        }
      
    }
}
