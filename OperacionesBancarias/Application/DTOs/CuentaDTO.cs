using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CuentaDTO
    {
        public int Id { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public int? SaldoInicial { get; set; }
        public bool? Estado { get; set; }
        public int? IdCliente { get; set; }
    }
}
