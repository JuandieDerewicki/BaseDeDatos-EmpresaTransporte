using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? calidad_servicio { get; set; }

        [Required]
        [StringLength(50)]
        public string? tipo_servicio { get; set; }

        [Required]
        [StringLength(100)]
        public int disponibilidad { get; set; }

        [ForeignKey("Itinerario")]
        public int? id_itinerario { get; set; }
        public virtual Itinerario? Itinerario { get; set; }

        [ForeignKey("UnidadTransporte")]
        public int? id_unidadTransporte { get; set; }
        public virtual UnidadTransporte? UnidadTransporte { get; set; }
    }
}
