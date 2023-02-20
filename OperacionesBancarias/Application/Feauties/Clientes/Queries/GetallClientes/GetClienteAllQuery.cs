using Application.DTOs;
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

namespace Application.Feauties.Clientes.Queries.GetallClientes
{
    public class GetClienteAllQuery : IRequest<Response<List<ClienteDTO>>> 
    {
        public class GetClienteAllQueryHandler : IRequestHandler<GetClienteAllQuery, Response<List<ClienteDTO>>>
        {
            private readonly IRepositoryAsync<Cliente> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetClienteAllQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }
            public async Task<Response<List<ClienteDTO>>> Handle(GetClienteAllQuery request, CancellationToken cancellationToken)
            {
                var Clientes = await _repositoryAsync.ListAsync();              
                var clientesDTO = _mapper.Map<List<ClienteDTO>>(Clientes);
                return new Response<List<ClienteDTO>>(clientesDTO);
            }
        }
    }
}
