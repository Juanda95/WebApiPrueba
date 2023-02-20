using Application.Filters;
using Application.Interfaces;
using Application.Wrappers;
using Domain.entities;
using MediatR;
using System.Data.Entity.SqlServer.Utilities;

namespace Application.Feauties.Movimientos.Commands.UpdateMovimientoCommand
{
    public class UpdateMovimientoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int? Valor { get; set; }
    }

    public class UpdateMovimientoCommandHandlers : IRequestHandler<UpdateMovimientoCommand, Response<int>>
    {
        //private readonly IReadRepositoryAsync<Movimiento> _IReadrepositoryAsync;
        private readonly IRepositoryAsync<Movimiento> _repositoryAsync;


        public UpdateMovimientoCommandHandlers(/*IReadRepositoryAsync<Movimiento> _ireadrepositoryAsync,*/ IRepositoryAsync<Movimiento> repositoryAsync)
        {
            //_IReadrepositoryAsync = _ireadrepositoryAsync;
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateMovimientoCommand request, CancellationToken cancellationToken)
        {
            //var Movimiento = await _repositoryAsync.GetByIdAsync(request.Id);
            var Movimiento = await _repositoryAsync.FirstOrDefaultAsync( new consultaCuentas(request.Id));
            
            
            if (Movimiento == null)
            {

                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
             
                if (request.Valor > 0)
                {
                    int? saldoTotal = Movimiento.IdCuentaNavigation.SaldoInicial + request.Valor;
                    Movimiento.TipoMovimiento = "crédito";
                    Movimiento.Valor = request.Valor;
                    Movimiento.IdCuentaNavigation.SaldoInicial = saldoTotal;
                    Movimiento.Saldo = saldoTotal;

                }
                
                

                await _repositoryAsync.UpdateAsync(Movimiento);
                return new Response<int>(Movimiento.Id);
            }
        }
    }
}
