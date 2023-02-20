using Application.Exceptions;
using Application.Feauties.Clientes.Commands.CreateClienteCommand;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feauties.Cuentas.Common.CreateCuentaCommand
{
    public class CreateCuentaCommand: IRequest<Response<int>>
    {
        public string? NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public int? SaldoInicial { get; set; }
        public bool? Estado { get; set; }
        public int? IdCliente { get; set; }

    }

    public class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cuenta> _repositoryAsync;
        private readonly IRepositoryAsync<Movimiento> _repositoryAsyncMovimiento;
        private readonly IMapper _mapper;

        public CreateCuentaCommandHandler(IRepositoryAsync<Cuenta> repositoryAsync, IMapper mapper, IRepositoryAsync<Movimiento> repositoryAsyncMovimiento)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _repositoryAsyncMovimiento = repositoryAsyncMovimiento;
        }
        public async Task<Response<int>> Handle(CreateCuentaCommand request, CancellationToken cancellationToken)
        {
            var nuevaCuenta = _mapper.Map<Cuenta>(request);
            try
            {
               
                var data = await _repositoryAsync.AddAsync(nuevaCuenta);
                Movimiento MovimientoInicial = new Movimiento()
                {
                    Fecha = DateTime.Today,
                    TipoMovimiento = "Credito",
                    Valor = request.SaldoInicial,
                    Saldo = request.SaldoInicial,
                    IdCliente = request.IdCliente,
                    IdCuenta = data.Id
                };
                await _repositoryAsyncMovimiento.AddAsync(MovimientoInicial);

                return new Response<int>(data.Id);
            }          
            catch (Exception ex)
            {
      
                var nameObjeto = GetName(() => request.IdCliente);
                throw new ApiException($"Validar el objeto enviado y que el cliente  {nameObjeto}:{request.IdCliente} se encuentre registrado.", ex.InnerException ?? new Exception());
             
            }
        }
        static string GetName<T>(Expression<Func<T>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }
    }
}
