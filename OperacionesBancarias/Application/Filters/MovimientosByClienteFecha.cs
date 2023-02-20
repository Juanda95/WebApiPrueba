using Ardalis.Specification;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filters
{
    public class MovimientosByClienteFecha : Specification<Movimiento>, ISingleResultSpecification
    {
        public MovimientosByClienteFecha( int? IdCliente, DateTime fechaIni, DateTime fechaFin)
        {
            Query
                .Where(x => x.IdCliente.Equals(IdCliente) && x.Fecha >= fechaIni && x.Fecha <= fechaFin)
                .Include(x => x.IdClienteNavigation)
                .Include(X => X.IdCuentaNavigation);
        }
    }
}
