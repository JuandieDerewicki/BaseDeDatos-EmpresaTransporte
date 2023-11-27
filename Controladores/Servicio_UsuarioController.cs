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

        //[HttpPost]
        //public async Task<ActionResult<Servicio_Usuario>> AgregarServicioUsuario(Servicio_Usuario servicioUsuario)
        //{
        //    var nuevoServicioUsuario = await _servicioUsuarioService.AgregarServicioUsuarioAsync(servicioUsuario);
        //    return CreatedAtAction(nameof(ObtenerServicioUsuarioPorId), new { id = nuevoServicioUsuario.id }, nuevoServicioUsuario);
        //}

        [HttpPost("agregar-servicio-usuario-y-reserva")]
        public async Task<IActionResult> AgregarServicioUsuarioYReserva([FromBody] Servicio_Usuario servicioUsuario)
        {
            try
            {
                var nuevoServicioUsuario = await _servicioUsuarioService.AgregarServicioUsuarioYReservaAsync(servicioUsuario);
                return CreatedAtAction(nameof(ObtenerServicioUsuarioPorId), new { id = nuevoServicioUsuario.id }, nuevoServicioUsuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
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

        //[HttpGet("verificar-disponibilidad/{idServicio}")]
        //public async Task<IActionResult> VerificarDisponibilidad(int idServicio)
        //{
        //    var disponibilidad = await _servicioUsuarioService.VerificarDisponibilidadAsync(idServicio);
        //    return Ok(disponibilidad);
        //}

        //[HttpPost("realizar-reserva")]
        //public async Task<IActionResult> RealizarReserva([FromBody] Servicio_Usuario reserva)
        //{
        //    try
        //    {
        //        var nuevaReserva = await _servicioUsuarioService.RealizarReservaAsync(reserva);
        //        return Ok(nuevaReserva);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

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
