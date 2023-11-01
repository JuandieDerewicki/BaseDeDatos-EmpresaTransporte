using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ViajePlusBDAPI.Modelos
{
    public class Servicio_Cliente
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Cliente")]
        public int dni_cliente { get; set; }

        [ForeignKey("Servicio")]
        public int id_servicio { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Servicio? Servicio { get; set; }
    }
}
