namespace Domain.entities
{
    public class Cliente : Persona
    {
        public string? Clienteid { get; set; }
        public string? Contrasena { get; set; }
        public bool? Estado { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; } = new List<Cuenta>();
        public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
    }
}
