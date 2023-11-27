using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ViajePlusBDAPI.Modelos
{
    public class Itinerario_PuntoIntermedio
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Itinerario")]
        public int? id_itinerario { get; set; }

        [ForeignKey("PuntoIntermedio")]
        public int? id_puntoIntermedio { get; set; }

        [Required]
        public string hora_llegada_PI { get; set; }

        [Required]
        public string hora_salida_PI {  get; set; }  

        public virtual Itinerario? Itinerario { get; set; }
        public virtual PuntoIntermedio? PuntoIntermedio { get; set; }
    }
}
