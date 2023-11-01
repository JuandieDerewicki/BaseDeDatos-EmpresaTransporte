using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ViajePlusBDAPI.Modelos
{
    public class Itinerario_PuntoIntermedio
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Itinerario")]
        public int id_itinerario { get; set; }

        [ForeignKey("PuntoIntermedio")]
        public int id_puntoIntermedio { get; set; }

        public virtual Itinerario Itinerario { get; set; }
        public virtual PuntoIntermedio PuntoIntermedio { get; set; }
    }
}
