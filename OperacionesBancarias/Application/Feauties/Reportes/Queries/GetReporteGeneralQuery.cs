using Application.DTOs;
using Application.Feauties.Clientes.Queries.GetallClientes;
using Application.Filters;
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

namespace Application.Feauties.Reportes.Queries
{
    public class GetReporteGeneralQuery : IRequest<Response<List<ReporteClienteGeneralDTO>>>
    {
        public int ClienteId { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public class GetReporteGeneralHandler : IRequestHandler<GetReporteGeneralQuery, Response<List<ReporteClienteGeneralDTO>>>
        {
            private readonly IRepositoryAsync<Movimiento> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetReporteGeneralHandler(IRepositoryAsync<Movimiento> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }
            public async Task<Response<List<ReporteClienteGeneralDTO>>> Handle(GetReporteGeneralQuery request, CancellationToken cancellationToken)
            {
                var listaReportesClienteGeneralDTO = new List<ReporteClienteGeneralDTO>();
                var movimientos = await _repositoryAsync.ListAsync(new MovimientosByClienteFecha(request.ClienteId, request.FechaInicial, request.FechaFinal));
                foreach (var movimiento in movimientos)
                {
                    ReporteClienteGeneralDTO reporteClienteGeneralDTO = new ReporteClienteGeneralDTO()
                    {
                        Fecha = movimiento.Fecha.ToString(),
                        Cliente = movimiento?.IdClienteNavigation?.Nombre,
                        numeroCuenta = movimiento?.IdCuentaNavigation?.NumeroCuenta,
                        tipo = movimiento?.IdCuentaNavigation?.TipoCuenta,
                        SaldoInicial = movimiento?.Valor > 0 ? movimiento.Saldo - movimiento.Valor : movimiento?.Valor * (-1) + movimiento?.Saldo,
                        Estado = movimiento?.IdCuentaNavigation?.Estado.ToString(),
                        Movimiento = movimiento?.Valor,
                        SaldoDisponible = movimiento?.Saldo
                    };

                    listaReportesClienteGeneralDTO.Add(reporteClienteGeneralDTO);
                }

                return new Response<List<ReporteClienteGeneralDTO>>(listaReportesClienteGeneralDTO);
            }
        }

    }
}
