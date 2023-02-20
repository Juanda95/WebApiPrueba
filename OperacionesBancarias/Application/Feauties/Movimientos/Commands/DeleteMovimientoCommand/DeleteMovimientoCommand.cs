using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;

namespace Application.Feauties.Movimientos.Commands.DeleteMovimientoCommand
{
    public class DeleteMovimientoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteMovimientoCommandHandlers : IRequestHandler<DeleteMovimientoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Movimiento> _repositoryAsync;


        public DeleteMovimientoCommandHandlers(IRepositoryAsync<Movimiento> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;

        }


        public async Task<Response<int>> Handle(DeleteMovimientoCommand request, CancellationToken cancellationToken)
        {
            var Movimiento = await _repositoryAsync.GetByIdAsync(request.Id);
            if (Movimiento == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id{request.Id}");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(Movimiento);
                return new Response<int>(Movimiento.Id);
            }
        }

    }
}
