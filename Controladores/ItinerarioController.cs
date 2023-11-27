using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ViajePlusBDAPI.Modelos;
using ViajePlusBDAPI.Servicios;

namespace ViajePlusBDAPI.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItinerarioController : ControllerBase
    {
        IItinerarioService _itinerarioService;

        public ItinerarioController(IItinerarioService itinerarioService)
        {
            _itinerarioService = itinerarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Itinerario>>> ObtenerTodosItinerariosAsync()
        {
            var itinerarios = await _itinerarioService.ObtenerTodosItinerariosAsync();
            return Ok(itinerarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Itinerario>> ObtenerItinerarioPorIdAsync(int id)
        {
            var itinerario = await _itinerarioService.ObtenerItinerarioPorIdAsync(id);

            if (itinerario == null)
            {
                return NotFound();
            }

            return Ok(itinerario);
        }

        [HttpGet("ciudad/{ciudad}")]
        public async Task<ActionResult<List<Itinerario>>> ObtenerItinerariosPorCiudadAsync(string ciudad)
        {
            var itinerarios = await _itinerarioService.ObtenerItinerariosPorCiudadAsync(ciudad);

            if (itinerarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(itinerarios);
        }

        [HttpPost]
        public async Task<ActionResult<Itinerario>> AgregarItinerarioAsync(Itinerario itinerario)
        {
            try
            {
                var itinerarioAgregado = await _itinerarioService.AgregarItinerarioAsync(itinerario);
                return Ok(itinerarioAgregado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar itinerario: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Itinerario>> EditarItinerarioAsync(int id, Itinerario itinerario)
        {
            try
            {
                var itinerarioEditado = await _itinerarioService.EditarItinerarioAsync(id, itinerario);
                return Ok(itinerarioEditado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar itinerario: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarItinerarioAsync(int id)
        {
            try
            {
                await _itinerarioService.EliminarItinerarioAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar itinerario: {ex.Message}");
            }
        }
    }
}
