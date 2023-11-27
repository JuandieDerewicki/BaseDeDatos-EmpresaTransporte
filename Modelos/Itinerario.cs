using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViajePlusBDAPI.Modelos
{
    public class Itinerario
    {
        [Key]
        public int id_itinerario {  get; set; }

        [Required]
        [StringLength(30)]
        public string ciudad_origen { get; set; }

        [Required]
        [StringLength(30)]
        public string ciudad_destino {  get; set; }

        [Required]
        public string fecha_partida { get; set; }

        [Required]
        public string fecha_llegada { get; set; }

        [Required]
        public string hora_partida { get; set; }

        [Required]
        public string hora_llegada { get; set; }


        [JsonIgnore]
        public ICollection<Servicio>? Servicios { get; set; }

        [JsonIgnore]
        public ICollection<Itinerario_PuntoIntermedio>? Itinerario_PuntoIntermedios { get; set; }
    }
}
