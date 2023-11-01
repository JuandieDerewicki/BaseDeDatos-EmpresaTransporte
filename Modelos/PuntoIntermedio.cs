using System.ComponentModel.DataAnnotations;

namespace ViajePlusBDAPI.Modelos
{
    public class PuntoIntermedio
    {
        [Key]
        public int id_puntoIntermedio {  get; set; }

        [Required]
        [StringLength(50)]  
        public string nombre_ciudad {  get; set; }

    }
}
