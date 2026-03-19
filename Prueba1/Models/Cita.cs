namespace Prueba1.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public DateTime Fecha { get; set; }  
        public string Horario { get; set; }   
        public int Estatus { get; set; } = 1;
        public string NombrePaciente { get; set; }
        public string NombreMedico { get; set; }
        public string Especialidad { get; set; }
        public string EstatusDescripcion
        {
            get
            {
                return Estatus switch
                {
                    1 => "Activa",
                    2 => "Cancelada",
                    3 => "Pasada"
                };
            }
        }
    }
}
