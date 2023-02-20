using Application.Exceptions;
using Application.Filters;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Movimientos.Commands.CreateMovimientoCommand
{
    public class CreateMovimientoCommand : IRequest<Response<int>>
    {
        public DateTime? Fecha { get; set; }
        public int? Valor { get; set; }
        public int? IdCliente { get; set; }
        public int? IdCuenta { get; set; }
    }

    public class CreateMovimientoCommandHandler : IRequestHandler<CreateMovimientoCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Movimiento> _repositoryAsync;
        private readonly IRepositoryAsync<Cuenta> _repositoryAsyncCuenta;
        private readonly IMapper _mapper;

        public CreateMovimientoCommandHandler(IRepositoryAsync<Movimiento> repositoryAsync, IMapper mapper, IRepositoryAsync<Cuenta> repositoryAsyncCuenta)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _repositoryAsyncCuenta = repositoryAsyncCuenta;
        }
        public async Task<Response<int>> Handle(CreateMovimientoCommand request, CancellationToken cancellationToken)
        {
            string nameObjeto = "";
            Cuenta cuentaEstadoActual = await _repositoryAsyncCuenta.FirstOrDefaultAsync(new ConsultaCuentaByIdCliente_Cuenta(request.IdCuenta, request.IdCliente));
            int? SaldoUltimoMovimiento = cuentaEstadoActual?.Movimientos?.OrderByDescending(x => x.Id).FirstOrDefault()?.Saldo ?? 0;
            if (cuentaEstadoActual == null)
            {
                nameObjeto = GetName(() => request.IdCuenta);
                throw new ApiException($"verifique el siguiente campo {nameObjeto}:{request.IdCuenta}");
            }

            int? saldoTotal = 0;
            var nuevaMovimiento = _mapper.Map<Movimiento>(request);


            if (request.Valor > 0)
            {
                saldoTotal = SaldoUltimoMovimiento + request.Valor;
                nuevaMovimiento.TipoMovimiento = "Crédito";

            }
            else
            {
                var valorResta = request.Valor * (-1);
                nuevaMovimiento.TipoMovimiento = "Debito";
                saldoTotal = SaldoUltimoMovimiento - valorResta;
                if (saldoTotal < 0)
                {
                    throw new ApiException($"Saldo no disponible");
                }

            }
            try
            {
                
                nuevaMovimiento.Saldo = saldoTotal;
                await _repositoryAsyncCuenta.UpdateAsync(cuentaEstadoActual);
                var data = await _repositoryAsync.AddAsync(nuevaMovimiento);
                return new Response<int>(data.Id);
            }
            catch (Exception ex)
            {

                nameObjeto = GetName(() => request.IdCliente);
                throw new ApiException($"Validar el objeto enviado, segun el esquema establecido y que el cliente  {nameObjeto}:{request.IdCliente} se encuentre registrado con una cuneta activa", ex.InnerException ?? new Exception());

            }

        }


        static string GetName<T>(Expression<Func<T>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }
    }
}
