using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ViajePlusBDAPI.Modelos;
using ViajePlusBDAPI.Servicios;

namespace ViajePlusBDAPI.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Servicio>>> ObtenerTodosServiciosAsync()
        {
            var servicios = await _servicioService.ObtenerTodosServiciosAsync();
            return Ok(servicios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> ObtenerServicioPorIdAsync(int id)
        {
            var servicio = await _servicioService.ObtenerServicioPorIdAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return Ok(servicio);
        }

        //[HttpGet("itinerario/{idItinerario}")]
        //public async Task<ActionResult<List<Servicio>>> ObtenerServiciosPorItinerarioAsync(int idItinerario)
        //{
        //    var servicios = await _servicioService.ObtenerServiciosPorItinerarioAsync(idItinerario);

        //    if (servicios.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(servicios);
        //}


        //[HttpPost]
        //public async Task<ActionResult<Servicio>> AgregarServicioAsync([FromBody] Servicio nuevoServicio)
        //{
        //    try
        //    {
        //        var servicioAgregado = await _servicioService.AgregarServicioAsync(nuevoServicio);
        //        return servicioAgregado;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Cambiado para devolver más detalles sobre la excepción
        //        return StatusCode(500, $"Error al agregar el servicio: {ex.Message}\n{ex.StackTrace}");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<Servicio>> AgregarServicioAsync([FromBody] Servicio nuevoServicio)
        {
            try
            {
                // ...

                var itinerario = await _servicioService.ObtenerItinerarioAsync(nuevoServicio.id_itinerario ?? 0);
                var unidadTransporte = await _servicioService.ObtenerUnidadTransporteAsync(nuevoServicio.id_unidadTransporte ?? 0);

                if (itinerario == null)
                {
                    throw new Exception("El itinerario no existe");
                }

                if (unidadTransporte == null)
                {
                    throw new Exception("La unidad de transporte no existe");
                }

                nuevoServicio.Itinerario = itinerario;
                nuevoServicio.UnidadTransporte = unidadTransporte;

                var servicioAgregado = await _servicioService.AgregarServicioAsync(nuevoServicio);

                // ...

                return CreatedAtAction("ObtenerServicioPorId", new { id = servicioAgregado.id_servicio }, servicioAgregado);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Error al agregar el servicio: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Servicio>> EditarServicioAsync(int id, Servicio servicio)
        {
            try
            {
                var servicioEditado = await _servicioService.EditarServicioAsync(id, servicio);
                return Ok(servicioEditado);
            }
            catch (Exception ex)
            {
                return NotFound($"Servicio con ID {id} no encontrado");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarServicioAsync(int id)
        {
            try
            {
                await _servicioService.EliminarServicioAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
