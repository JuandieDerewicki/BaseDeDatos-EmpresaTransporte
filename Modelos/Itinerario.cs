using System.ComponentModel.DataAnnotations;

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
    }
}
