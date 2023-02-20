using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClienteDTO
    {
        public  int Id { get; set; }
        public string? Nombre { get; set; }

        public string? Genero { get; set; }

        public int? Edad { get; set; }

        public string? Identificacion { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Clienteid { get; set; }

        public string? Contrasena { get; set; }

        public string? Estado { get; set; }
    }
}
