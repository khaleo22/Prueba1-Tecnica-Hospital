using System.ComponentModel.DataAnnotations;

namespace Prueba1.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string APaterno { get; set; }

        [StringLength(50)]
        public string AMaterno { get; set; }

        [StringLength(50)]
        public string Cedula { get; set; }

        [Required]
        public int IdEspecialista { get; set; }

        [StringLength(50)]
        public string EspecialidadConcepto { get; set; }
    }
}