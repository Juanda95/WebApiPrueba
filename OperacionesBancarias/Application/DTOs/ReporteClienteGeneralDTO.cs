namespace Application.DTOs
{
    public class ReporteClienteGeneralDTO
    {
        public string? Fecha { get; set; }
        public string? Cliente { get; set; }
        public string? numeroCuenta { get; set; }
        public string? tipo { get; set; }
        public int? SaldoInicial { get; set;}
        public string? Estado { get; set; }
        public int? Movimiento { get; set; }
        public int? SaldoDisponible { get; set; }

    }
}
