using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;

namespace Application.Feauties.Cuentas.Queries.GetallCuentas
{
    public class GetCuentasAllQuery : IRequest<Response<List<CuentaDTO>>>
    {
        public class GetCuentaAllQueryHandler : IRequestHandler<GetCuentasAllQuery, Response<List<CuentaDTO>>>
        {
            private readonly IRepositoryAsync<Cuenta> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetCuentaAllQueryHandler(IRepositoryAsync<Cuenta> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }
            public async Task<Response<List<CuentaDTO>>> Handle(GetCuentasAllQuery request, CancellationToken cancellationToken)
            {
                var Cuentas = await _repositoryAsync.ListAsync();
                var CuentasDTO = _mapper.Map<List<CuentaDTO>>(Cuentas);
                return new Response<List<CuentaDTO>>(CuentasDTO);
            }
        }
    }
}
