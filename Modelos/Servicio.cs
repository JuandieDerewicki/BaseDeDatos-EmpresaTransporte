using System.ComponentModel.DataAnnotations;

namespace ViajePlusBDAPI.Modelos
{
    public class Servicio
    {
        [Key]
        public int id_servicio {  get; set; }

        [Required]
        [StringLength(100)]
        public string fecha_partida { get; set; }

        [Required]
        [StringLength(100)]
        public string fecha_llegada { get; set; }

        [Required]
        [StringLength(50)]
        public string calidad_servicio { get; set; }

        [Required]
        [StringLength(50)]
        public string tipo_servicio { get; set; }

        [Required]
        [StringLength(100)]
        public int disponibilidad { get; set; }

        // Faltan agregar FKS
    }
}
