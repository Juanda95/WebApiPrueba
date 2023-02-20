using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Cuenta 
    {
        public int Id { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public int? SaldoInicial { get; set; }
        public bool? Estado { get; set; }
        public int? IdCliente { get; set; }
        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
    }
}
