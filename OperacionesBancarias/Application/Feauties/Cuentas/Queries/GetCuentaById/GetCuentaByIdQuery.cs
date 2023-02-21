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

namespace Application.Feauties.Cuentas.Queries.GetClienteById
{
    public class GetCuentaByIdQuery : IRequest<Response<CuentaDTO>>
    {
        public int Id { get; set; }

        public class GetCuentaByIdQueryHandler : IRequestHandler<GetCuentaByIdQuery, Response<CuentaDTO>>
        {
            private readonly IRepositoryAsync<Cuenta> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetCuentaByIdQueryHandler(IRepositoryAsync<Cuenta> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<CuentaDTO>> Handle(GetCuentaByIdQuery request, CancellationToken cancellationToken)
            {
                var Cuenta = await _repositoryAsync.GetByIdAsync(request.Id);
                if (Cuenta == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<CuentaDTO>(Cuenta);
                    return new Response<CuentaDTO>(dto);
                }

            }
        }
    }
}
