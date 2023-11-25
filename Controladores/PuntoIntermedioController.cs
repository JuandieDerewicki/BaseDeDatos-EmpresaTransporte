using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ViajePlusBDAPI.Modelos;
using ViajePlusBDAPI.Servicios;

namespace ViajePlusBDAPI.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PuntoIntermedioController : ControllerBase
    {
        IPuntoIntermedioService _puntoIntermedioService;
        public PuntoIntermedioController(IPuntoIntermedioService puntoIntermedioService)
        {
            _puntoIntermedioService = puntoIntermedioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PuntoIntermedio>>> ObtenerTodosPuntosIntermediosAsync()
        {
            var puntosIntermedios = await _puntoIntermedioService.ObtenerTodosPuntosIntermediosAsync();
            return Ok(puntosIntermedios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PuntoIntermedio>> ObtenerPuntoIntermedioPorIdAsync(int id)
        {
            var puntoIntermedio = await _puntoIntermedioService.ObtenerPuntoIntermedioPorIdAsync(id);

            if (puntoIntermedio == null)
            {
                return NotFound();
            }

            return Ok(puntoIntermedio);
        }

        [HttpGet("ObtenerPuntosIntermediosPorNombre/{nombre}")]
        public async Task<ActionResult<PuntoIntermedio>> ObtenerPuntoIntermedioPorNombreAsync(string nombre)
        {
            var puntoIntermedio = await _puntoIntermedioService.ObtenerPuntoIntermedioPorNombreAsync(nombre);

            if (puntoIntermedio == null)
            {
                return NotFound();
            }

            return Ok(puntoIntermedio);
        }

        [HttpPost]
        public async Task<ActionResult<PuntoIntermedio>> AgregarPuntoIntermedioAsync(PuntoIntermedio puntoIntermedio)
        {
            try
            {
                var puntoIntermedioAgregado = await _puntoIntermedioService.AgregarPuntoIntermedioAsync(puntoIntermedio);
                return Ok(puntoIntermedioAgregado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar punto intermedio: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PuntoIntermedio>> EditarPuntoIntermedioAsync(int id, PuntoIntermedio puntoIntermedio)
        {
            try
            {
                var puntoIntermedioEditado = await _puntoIntermedioService.EditarPuntoIntermedioAsync(id, puntoIntermedio);
                return Ok(puntoIntermedioEditado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar punto intermedio: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPuntoIntermedioAsync(int id)
        {
            try
            {
                await _puntoIntermedioService.EliminarPuntoIntermedioAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar punto intermedio: {ex.Message}");
            }
        }
    }
}

