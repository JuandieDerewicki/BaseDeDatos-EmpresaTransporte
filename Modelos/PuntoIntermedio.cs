using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViajePlusBDAPI.Modelos
{
    public class PuntoIntermedio
    {
        [Key]
        public int id_puntoIntermedio {  get; set; }

        [Required]
        [StringLength(50)]  
        public string nombre_ciudad {  get; set; }

        [JsonIgnore]
        public ICollection<Itinerario_PuntoIntermedio>? Itinerario_PuntoIntermedios { get; set; }
    }
}
