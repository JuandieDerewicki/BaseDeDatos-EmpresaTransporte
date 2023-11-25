using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ViajePlusBDAPI.Modelos;
using ViajePlusBDAPI.Servicios;

namespace ViajePlusBDAPI.Controladores
{
    [EnableCors("ReglasCors")]
    // El controlador RolesController es responsable de gestionar los roles
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
        {
        // Esto permite que el controlador utilice los servicios proporcionados por RolesService para realizar operaciones relacionadas con los roles.
        IRolService _rolesService;

        public RolController(IRolService rolesService)
        {
            _rolesService = rolesService;
        }

        // Obtiene todos los roles existentes
        [HttpGet]
        public async Task<ActionResult<List<Rol>>> ObtenerTodosRolesAsync()
        {
            var roles = await _rolesService.ObtenerTodosRolesAsync();
            return Ok(roles);
        }

        // Lo mismo pero los obtiene por id
        [HttpGet("ObtenerRolPorId/{id}")]
        public async Task<ActionResult<Rol>> ObtenerRolPorIdAsync(int id)
        {
            var rol = await _rolesService.ObtenerRolPorIdAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> CrearRolAsync(Rol rol)
        {
            var rolCreado = await _rolesService.CrearRolAsync(rol);

            if (rolCreado == null)
            {
                return BadRequest(); // Otra respuesta apropiada si la creación falla.
            }

            return CreatedAtRoute(new { id = rolCreado.id_rol }, rolCreado);
        }

        [HttpGet("ObtenerTipoDeRol/{tipoRol}")]
        public async Task<ActionResult<Rol>> ObtenerRolPorTipoAsync(string tipoRol)
        {
            var rol = await _rolesService.ObtenerRolPorTipoAsync(tipoRol);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }
    }
}

