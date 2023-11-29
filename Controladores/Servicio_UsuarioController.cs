using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ViajePlusBDAPI.Modelos;
using ViajePlusBDAPI.Servicios;

namespace ViajePlusBDAPI.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class Servicio_UsuarioController : ControllerBase
    {
        private readonly IServicioUsuarioService _servicioUsuarioService;

        public Servicio_UsuarioController(IServicioUsuarioService servicioUsuarioService)
        {
            _servicioUsuarioService = servicioUsuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerTodosServiciosUsuarios()
        {
            var serviciosUsuarios = await _servicioUsuarioService.ObtenerTodosServiciosUsuariosAsync();
            return Ok(serviciosUsuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio_Usuario>> ObtenerServicioUsuarioPorId(int id)
        {
            var servicioUsuario = await _servicioUsuarioService.ObtenerServicioUsuarioPorIdAsync(id);
            if (servicioUsuario == null)
            {
                return NotFound();
            }
            return Ok(servicioUsuario);
        }

        [HttpGet("usuario/{dniUsuario}")]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerServiciosUsuarioPorDni(string dniUsuario)
        {
            var serviciosUsuario = await _servicioUsuarioService.ObtenerServiciosUsuarioPorDniAsync(dniUsuario);
            return Ok(serviciosUsuario);
        }

        [HttpGet("usuario/{idservicio}")]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerServiciosUsuarioPorServicio(int idservicio)
        {
            var serviciosUsuario = await _servicioUsuarioService.ObtenerServiciosUsuarioPorServicioAsync(idservicio);
            return Ok(serviciosUsuario);
        }


        [HttpGet("ventas")]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerServiciosVentaTrue()
        {
            var serviciosVentaTrue = await _servicioUsuarioService.ObtenerServiciosVentaTrueAsync();
            return Ok(serviciosVentaTrue);
        }

        [HttpGet("reservas")]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerServiciosReservados()
        {
            var serviciosReservados = await _servicioUsuarioService.ObtenerServiciosReservadosAsync();
            return Ok(serviciosReservados);
        }

        [HttpGet("pasajesVendidos/cama")]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerPasajesVendidosCama()
        {
            var pasajesVendidosCama = await _servicioUsuarioService.ObtenerPasajesVendidosCamaAsync();
            return Ok(pasajesVendidosCama);
        }

        [HttpGet("pasajesVendidos/semicama")]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerPasajesVendidosSemicama()
        {
            var pasajesVendidosSemicama = await _servicioUsuarioService.ObtenerPasajesVendidosSemicamaAsync();
            return Ok(pasajesVendidosSemicama);
        }

        [HttpGet("pasajesVendidos/comun")]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerPasajesVendidosComun()
        {
            var pasajesVendidosComun = await _servicioUsuarioService.ObtenerPasajesVendidosComunAsync();
            return Ok(pasajesVendidosComun);
        }

        [HttpGet("pasajesVendidos/conItinerario")]
        public async Task<ActionResult<List<Servicio_Usuario>>> ObtenerPasajesVendidosConItinerario()
        {
            var pasajesVendidosConItinerario = await _servicioUsuarioService.ObtenerPasajesVendidosConItinerarioAsync();
            return Ok(pasajesVendidosConItinerario);
        }


        [HttpPost("agregar-servicio-usuario-y-reserva")]
        public async Task<IActionResult> AgregarServicioUsuarioYReserva([FromBody] Servicio_Usuario servicioUsuario)
        {
            try
            {
                var nuevoServicioUsuario = await _servicioUsuarioService.ReservaPasajeAsync(servicioUsuario);
                return CreatedAtAction(nameof(ObtenerServicioUsuarioPorId), new { id = nuevoServicioUsuario.id }, nuevoServicioUsuario);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = $"Error al agregar el servicioUsuario: {ex.ParamName} - {ex.Message} {ex.InnerException}" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = $"Error al agregar el servicioUsuario: {ex.Message} {ex.InnerException}" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno. Por favor, inténtelo de nuevo más tarde." });
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<Servicio_Usuario>> EditarServicioUsuario(int id, Servicio_Usuario servicioUsuario)
        {
            try
            {
                var servicioUsuarioEditado = await _servicioUsuarioService.EditarServicioUsuarioAsync(id, servicioUsuario);
                return Ok(servicioUsuarioEditado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarServicioUsuario(int id)
        {
            await _servicioUsuarioService.EliminarServicioUsuarioAsync(id);
            return NoContent();
        }


        [HttpDelete("cancelar-reserva/{reservaId}")]
        public async Task<IActionResult> CancelarReserva(int reservaId)
        {
            try
            {
                await _servicioUsuarioService.CancelarReservaAsync(reservaId);
                return Ok(new { message = "Reserva cancelada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
