using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViajePlusBDAPI.Modelos
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string dni { get; set; } // Clave Primaria 

        [Required]
        [StringLength(100)]
        public string nombreCompleto { get; set; }


        [Required]
        [StringLength(20)]
        public string fechaNacimiento { get; set; }

        [Required]
        [StringLength(50)]
        public string direccion { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        [StringLength(15)]
        public string telefono { get; set; }

        [Required]
        [StringLength(30)]
        public string tipo_usuario { get; set; }

        [Required]
        [StringLength(64)]
        public string contraseña { get; set; }

        [JsonIgnore]
        public ICollection<Servicio_Usuario>? Servicio_Usuarios { get; set; }
    }
}
