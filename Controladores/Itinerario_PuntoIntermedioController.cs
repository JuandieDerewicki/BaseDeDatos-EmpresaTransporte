using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ViajePlusBDAPI.Modelos;
using ViajePlusBDAPI.Servicios;

namespace ViajePlusBDAPI.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class Itinerario_PuntoIntermedioController : ControllerBase
    {
        IItinerarioPuntoIntermedioService _itinerarioPuntoIntermedioService;

        public Itinerario_PuntoIntermedioController(IItinerarioPuntoIntermedioService itinerarioPuntoIntermedioService)
        {
            _itinerarioPuntoIntermedioService = itinerarioPuntoIntermedioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Itinerario_PuntoIntermedio>>> ObtenerTodosItinerariosPuntoIntermedioAsync()
        {
            var itinerariosPuntoIntermedio = await _itinerarioPuntoIntermedioService.ObtenerTodosItinerariosPuntoIntermedioAsync();
            return Ok(itinerariosPuntoIntermedio);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Itinerario_PuntoIntermedio>> ObtenerItinerarioPuntoIntermedioPorIdAsync(int id)
        {
            var itinerarioPuntoIntermedio = await _itinerarioPuntoIntermedioService.ObtenerItinerarioPuntoIntermedioPorIdAsync(id);

            if (itinerarioPuntoIntermedio == null)
            {
                return NotFound();
            }

            return Ok(itinerarioPuntoIntermedio);
        }

        [HttpGet("ciudad/{ciudad}")]
        public async Task<ActionResult<List<Itinerario_PuntoIntermedio>>> ObtenerItinerariosPuntoIntermedioPorCiudadAsync(string ciudad)
        {
            var itinerariosPuntoIntermedio = await _itinerarioPuntoIntermedioService.ObtenerItinerariosPuntoIntermedioPorCiudadAsync(ciudad);

            if (itinerariosPuntoIntermedio.Count == 0)
            {
                return NotFound();
            }

            return Ok(itinerariosPuntoIntermedio);
        }

        [HttpGet("ciudades")]
        public async Task<ActionResult<List<Itinerario_PuntoIntermedio>>> ObtenerItinerariosPuntoIntermedioPorCiudadOrigenYDestinoAsync([FromQuery] string ciudadOrigen, [FromQuery] string ciudadDestino)
        {
            try
            {
                var itinerariosPuntoIntermedio = await _itinerarioPuntoIntermedioService.ObtenerItinerariosPuntoIntermedioPorCiudadOrigenYDestinoAsync(ciudadOrigen, ciudadDestino);

                if (itinerariosPuntoIntermedio.Count == 0)
                {
                    return NotFound();
                }

                return Ok(itinerariosPuntoIntermedio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener Itinerarios_PuntosIntermedios: {ex.Message}");
            }
        }


        [HttpGet("puntos-intermedios/{idItinerario}")]
        public async Task<ActionResult<List<Itinerario_PuntoIntermedio>>> ObtenerPuntosIntermediosPorItinerario(int idItinerario)
        {
            try
            {
                var puntosIntermedios = await _itinerarioPuntoIntermedioService.ObtenerPuntosIntermediosPorItinerarioAsync(idItinerario);
                return Ok(puntosIntermedios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener puntos intermedios: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Itinerario_PuntoIntermedio>> AgregarItinerarioPuntoIntermedioAsync(Itinerario_PuntoIntermedio itinerarioPuntoIntermedio)
        {
            try
            {
                var itinerarioPuntoIntermedioAgregado = await _itinerarioPuntoIntermedioService.AgregarItinerarioPuntoIntermedioAsync(itinerarioPuntoIntermedio);
                return Ok(itinerarioPuntoIntermedioAgregado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar Itinerario_PuntoIntermedio: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Itinerario_PuntoIntermedio>> EditarItinerarioPuntoIntermedioAsync(int id, Itinerario_PuntoIntermedio itinerarioPuntoIntermedio)
        {
            try
            {
                var itinerarioPuntoIntermedioEditado = await _itinerarioPuntoIntermedioService.EditarItinerarioPuntoIntermedioAsync(id, itinerarioPuntoIntermedio);
                return Ok(itinerarioPuntoIntermedioEditado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar Itinerario_PuntoIntermedio: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarItinerarioPuntoIntermedioAsync(int id)
        {
            try
            {
                await _itinerarioPuntoIntermedioService.EliminarItinerarioPuntoIntermedioAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar Itinerario_PuntoIntermedio: {ex.Message}");
            }
        }
    }
}
