using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitsTests.Mappings
{
    public class MappingsTesting
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        public MappingsTesting()
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

    }
}
