using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Movimiento: Entity
    {
        public DateTime? Fecha { get; set; }

        public string? TipoMovimiento { get; set; }

        public int? Valor { get; set; }

        public int? Saldo { get; set; }

        public int? IdCliente { get; set; }

        public int? IdCuenta { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }

        public virtual Cuenta? IdCuentaNavigation { get; set; }
    }
}
