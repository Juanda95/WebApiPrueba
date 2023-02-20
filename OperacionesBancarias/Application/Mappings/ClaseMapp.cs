using Application.DTOs;
using Application.Feauties.Clientes.Commands.CreateClienteCommand;
using Application.Feauties.Cuentas.Common.CreateCuentaCommand;
using Application.Feauties.Movimientos.Commands.CreateMovimientoCommand;
using AutoMapper;
using Domain.entities;

namespace Application.Mappings
{
    public class ClaseMapp : Profile
    {
        public ClaseMapp()
        {
            #region Commands
            CreateMap<CreateClienteCommand, Cliente>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Movimientos, opt => opt.Ignore())
                .ForMember(x => x.Cuenta, opt => opt.Ignore());

            CreateMap<CreateCuentaCommand, Cuenta>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Movimientos, opt => opt.Ignore())
                .ForMember(x => x.IdClienteNavigation, opt => opt.Ignore());

            CreateMap<CreateMovimientoCommand, Movimiento>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.TipoMovimiento, opt => opt.Ignore())
                .ForMember(x => x.Saldo, opt => opt.Ignore())
                .ForMember(x => x.IdClienteNavigation, opt => opt.Ignore())
                .ForMember(x => x.IdCuentaNavigation, opt => opt.Ignore());
            #endregion
            #region DTOs
            CreateMap<Cliente, ClienteDTO>();
            #endregion
        }
    }
}
