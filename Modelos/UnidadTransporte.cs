using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViajePlusBDAPI.Modelos
{
    public class UnidadTransporte
    {
        [Key]
        public int id_unidadTransporte {  get; set; }

        [Required]
        [StringLength(50)]
        public string tipo_unidad {  get; set; }

        [Required]
        [StringLength(50)]
        public string categoria { get; set; }

        public int asientos { get; set; }

        [JsonIgnore]
        public ICollection<Servicio>? Servicios { get; set; }
    }
}
