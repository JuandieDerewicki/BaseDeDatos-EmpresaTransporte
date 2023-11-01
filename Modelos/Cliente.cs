using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ViajePlusBDAPI.Modelos
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string dni { get; set; } // Clave Primaria 

        [Required]
        [StringLength(100)]
        public string nombreCompleto { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        [StringLength(20)]
        public string fechaNacimiento { get; set; }

        [Required]
        [StringLength(50)]
        public string direccion { get; set; }

        [Required]
        [StringLength(64)]
        public string contraseña { get; set; }

        [Required]
        [StringLength(15)]
        public string telefono { get; set; }
    }
}
