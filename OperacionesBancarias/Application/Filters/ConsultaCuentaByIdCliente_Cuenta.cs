using Ardalis.Specification;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filters
{
    public  class ConsultaCuentaByIdCliente_Cuenta :Specification<Cuenta>, ISingleResultSpecification
    {
        public ConsultaCuentaByIdCliente_Cuenta(int? IdCuenta, int? IdCliente)
        {
            Query
                .Where(x => x.IdCliente.Equals(IdCliente) && x.Id.Equals(IdCuenta))
                .Include(x => x.Movimientos);                        
        }
    }
}
