using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ViajePlusBDAPI.Modelos;
using ViajePlusBDAPI.Servicios;

namespace ViajePlusBDAPI.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadTransporteController : ControllerBase
    {
        IUnidadTransporteService _unidadTransporteService;

        public UnidadTransporteController(IUnidadTransporteService unidadTransporteService)
        {
            _unidadTransporteService = unidadTransporteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnidadTransporte>>> ObtenerTodasUnidadesTransporteAsync()
        {
            var unidadesTransporte = await _unidadTransporteService.ObtenerTodasUnidadesTransporteAsync();
            return Ok(unidadesTransporte);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadTransporte>> ObtenerUnidadTransportePorIdAsync(int id)
        {
            var unidadTransporte = await _unidadTransporteService.ObtenerUnidadTransportePorIdAsync(id);

            if (unidadTransporte == null)
            {
                return NotFound();
            }

            return Ok(unidadTransporte);
        }

        [HttpPost]
        public async Task<ActionResult<UnidadTransporte>> AgregarUnidadTransporteAsync(UnidadTransporte unidadTransporte)
        {
            try
            {
                var unidadTransporteAgregada = await _unidadTransporteService.AgregarUnidadTransporteAsync(unidadTransporte);
                return Ok(unidadTransporteAgregada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar unidad de transporte: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UnidadTransporte>> EditarUnidadTransporteAsync(int id, UnidadTransporte unidadTransporte)
        {
            try
            {
                var unidadTransporteEditada = await _unidadTransporteService.EditarUnidadTransporteAsync(id, unidadTransporte);
                return Ok(unidadTransporteEditada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar unidad de transporte: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUnidadTransporteAsync(int id)
        {
            try
            {
                await _unidadTransporteService.EliminarUnidadTransporteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar unidad de transporte: {ex.Message}");
            }
        }
    }
}
