using Ardalis.Specification;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filters
{
    public class consultaCuentas : Specification<Movimiento>, ISingleResultSpecification
    {
        public consultaCuentas(int Id)
        {
            Query.Where(x => x.Id == Id).
                Include(X => X.IdCuentaNavigation);
                  
        }
    }
}
