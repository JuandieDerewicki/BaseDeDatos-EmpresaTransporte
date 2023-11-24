using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Azure.Identity;

namespace ViajePlusBDAPI.Modelos
{
    public class Servicio_Usuario
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Usuario")]
        public string? dni_usuario { get; set; }

        [ForeignKey("Servicio")]
        public int? id_servicio { get; set; }

        [ForeignKey("PuntoIntermedio")]
        public int? id_puntoIntermedio { get; set; }

        public string tipo_atencion {  get; set; }

        public double costo_final {  get; set; }

        public virtual Usuario? Usuario { get; set; }
        public virtual Servicio? Servicio { get; set; }
        public virtual PuntoIntermedio? PuntoIntermedio { get; set;  }
    }
}
