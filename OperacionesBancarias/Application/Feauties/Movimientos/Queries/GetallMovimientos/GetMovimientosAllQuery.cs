using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;

namespace Application.Feauties.Movimientos.Queries.GetallMovimientos
{
    public class GetMovimientosAllQuery : IRequest<Response<List<MovimientoDTO>>>
    {
        public class GetMovimientoAllQueryHandler : IRequestHandler<GetMovimientosAllQuery, Response<List<MovimientoDTO>>>
        {
            private readonly IRepositoryAsync<Movimiento> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetMovimientoAllQueryHandler(IRepositoryAsync<Movimiento> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }
            public async Task<Response<List<MovimientoDTO>>> Handle(GetMovimientosAllQuery request, CancellationToken cancellationToken)
            {
                var Movimientos = await _repositoryAsync.ListAsync();
                var MovimientosDTO = _mapper.Map<List<MovimientoDTO>>(Movimientos);
                return new Response<List<MovimientoDTO>>(MovimientosDTO);
            }
        }
    }
}
