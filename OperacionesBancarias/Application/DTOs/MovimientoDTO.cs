using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MovimientoDTO
    {
        public DateTime? Fecha { get; set; }

        public string? TipoMovimiento { get; set; }

        public int? Valor { get; set; }

        public int? Saldo { get; set; }

        public int? IdCliente { get; set; }

        public int? IdCuenta { get; set; }
    }
}
