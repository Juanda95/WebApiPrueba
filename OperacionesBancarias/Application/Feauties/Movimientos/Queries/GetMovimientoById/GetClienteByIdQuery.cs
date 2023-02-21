using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;

namespace Application.Feauties.Movimientos.Queries.GetMovimientoById
{
    public class GetMovimientoByIdQuery : IRequest<Response<MovimientoDTO>>
    {
        public int Id { get; set; }

        public class GetMovimientoByIdQueryHandler : IRequestHandler<GetMovimientoByIdQuery, Response<MovimientoDTO>>
        {
            private readonly IRepositoryAsync<Movimiento> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetMovimientoByIdQueryHandler(IRepositoryAsync<Movimiento> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoDTO>> Handle(GetMovimientoByIdQuery request, CancellationToken cancellationToken)
            {
                var Movimiento = await _repositoryAsync.GetByIdAsync(request.Id);
                if (Movimiento == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<MovimientoDTO>(Movimiento);
                    return new Response<MovimientoDTO>(dto);
                }

            }
        }
    }
}
