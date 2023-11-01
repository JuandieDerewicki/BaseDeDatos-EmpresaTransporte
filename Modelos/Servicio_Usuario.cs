using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ViajePlusBDAPI.Modelos
{
    public class Servicio_Usuario
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Usuario")]
        public int? dni_usuario { get; set; }

        [ForeignKey("Servicio")]
        public int? id_servicio { get; set; }

        [Required]
        [StringLength(30)]
        public double costo {  get; set; }

        [Required]
        [StringLength(50)]
        public string destino { get; set; }

        public virtual Usuario? Usuario { get; set; }
        public virtual Servicio? Servicio { get; set; }
    }
}
