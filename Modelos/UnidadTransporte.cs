using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(20)]
        public int asientos { get; set; }



    }
}
