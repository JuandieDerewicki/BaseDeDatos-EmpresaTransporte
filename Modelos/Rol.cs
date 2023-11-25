using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViajePlusBDAPI.Modelos
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_rol { get; set; } // Clave primaria 

        [Required]
        [StringLength(40)]
        public string tipo_rol { get; set; } // Almacena el tipo de rol que puede ser "Autoridades", "Docentes", "Padres", "Alumnos"


        [JsonIgnore]
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
