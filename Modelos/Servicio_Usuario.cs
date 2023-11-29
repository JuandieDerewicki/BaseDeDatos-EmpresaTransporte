using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Azure.Identity;
using System.Text.Json.Serialization;

namespace ViajePlusBDAPI.Modelos
{
    public class Servicio_Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("Usuario")]
        public string? dni_usuario { get; set; }

        [ForeignKey("Servicio")]
        public int? id_servicio { get; set; }

        [ForeignKey("PuntoIntermedio")]
        public int? id_puntoIntermedio { get; set; }

        public string tipo_atencion {  get; set; }

        public bool venta {  get; set; }
        public double? costo_final {  get; set; }

        public Usuario? Usuario { get; set; }
        public Servicio? Servicio { get; set; }
        public PuntoIntermedio? PuntoIntermedio { get; set;  }

   
    }
}
