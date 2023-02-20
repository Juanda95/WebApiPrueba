using Application.DTOs;
using Application.Feauties.Clientes.Commands.CreateClienteCommand;
using Application.Feauties.Cuentas.Common.CreateCuentaCommand;
using Application.Feauties.Movimientos.Commands.CreateMovimientoCommand;
using Application.Mappings;
using AutoMapper;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configurationProvider = new MapperConfiguration(cgf =>
            {
                cgf.AddProfile<ClaseMapp>();
            });

            _mapper = _configurationProvider.CreateMapper();
        }

        [Fact]
        public void ShouldBevaleConfiguration()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }


        [Theory]
        [InlineData(typeof(CreateClienteCommand), typeof(Cliente))]
        [InlineData(typeof(CreateCuentaCommand), typeof(Cuenta))]
        [InlineData(typeof(CreateMovimientoCommand), typeof(Movimiento))]
        [InlineData(typeof(Cliente), typeof(ClienteDTO))]
        public void Map_SorceToDestination_ExistConfiguration(Type origin, Type destino)
        {
           var instance = FormatterServices.GetSafeUninitializedObject(origin);

            _mapper.Map(instance, origin, destino);

        }
    }
}