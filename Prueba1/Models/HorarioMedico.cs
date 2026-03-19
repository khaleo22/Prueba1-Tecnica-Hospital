namespace Prueba1.Models
{
    public class HorarioMedico
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public string Dia { get; set; }
        public string HoraInicio { get; set; }   // antes TimeSpan
        public string HoraFin { get; set; }    // antes TimeSpan
        public int Estatus { get; set; }
        public string NombreMedico { get; set; }
    }
}
