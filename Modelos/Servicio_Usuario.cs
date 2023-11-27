using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Azure.Identity;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public double? costo_final {  get; set; }

        public virtual Usuario? Usuario { get; set; }
        public virtual Servicio? Servicio { get; set; }
        public virtual PuntoIntermedio? PuntoIntermedio { get; set;  }

        public void CalcularCostoFinal(Servicio servicio)
        {
            // Obtener el costo predeterminado del servicio
            double costoPredeterminado = servicio.costo_predeterminado ?? 0;

            // Aplicar el aumento por tipo de atención
            if (tipo_atencion == "Ejecutivo")
            {
                costoPredeterminado *= 1.20; // Aumento del 20% para atención ejecutiva
            }

            // Obtener la categoría de la unidad de transporte asociada al servicio
            string categoriaTransporte = servicio.UnidadTransporte?.categoria ?? "";

            // Aplicar el aumento por categoría de la unidad de transporte
            switch (categoriaTransporte)
            {
                case "Comun":
                    // No hay aumento
                    break;
                case "Semicama":
                    costoPredeterminado *= 1.10; // Aumento del 10% para categoría semicama
                    break;
                case "Cochecama":
                    costoPredeterminado *= 1.30; // Aumento del 30% para categoría cochecama
                    break;
                    // Puedes agregar más casos según las categorías que tengas
            }

            // Verificar si hay un punto intermedio asociado
            if (PuntoIntermedio == null)
            {
                // No hay punto intermedio, servicio completo, no hay descuento
            }
            else
            {
                // Hay un punto intermedio, se aplica un descuento del 10%
                costoPredeterminado *= 0.90;
            }

            // Asignar el costo final calculado
            costo_final = costoPredeterminado;
        }
    }
}
