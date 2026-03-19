namespace Prueba1.Models
{
    public class HistorialCita
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Horario { get; set; }
        public string NombreMedico { get; set; }
        public string Especialidad { get; set; }
        public int Estatus { get; set; } // 1 = Activa, 0 = Cancelada
    }
}
