using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ViajePlusBDAPI.Modelos
{
    public class Servicio
    {
        [Key]
        public int id_servicio {  get; set; }

        [Required]
        public double? costo_predeterminado { get; set; }

        [JsonIgnore]
        public int? disponibilidad { get; set; }


        [ForeignKey("Itinerario")]
        public int? id_itinerario { get; set; }

        [ForeignKey("UnidadTransporte")]

        public int? id_unidadTransporte { get; set; }


        public virtual Itinerario? Itinerario { get; set; }
        public virtual UnidadTransporte? UnidadTransporte { get; set; }

        [JsonIgnore]
        public ICollection<Servicio_Usuario>? Servicio_Usuarios { get; set; }
    }
}
