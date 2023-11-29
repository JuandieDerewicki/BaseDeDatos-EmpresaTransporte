using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using ViajePlusBDAPI.Modelos;
using ViajePlusBDAPI.Servicios;


namespace ViajePlusBDAPI.Controladores
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    // El controlador UsuariosController se encarga de gestionar las solicitudes relacionadas con los usuarios. 
    public class UsuariosController : ControllerBase
    {
        // Esto permite que el controlador utilice los servicios para realizar operaciones relacionadas con los Usuarios
        IUsuarioService _usuariosService;
        IRolService _rolesService;
        public UsuariosController(IUsuarioService usuariosService, IRolService rolesService)
        {
            _usuariosService = usuariosService;
            _rolesService = rolesService;
        }

        // Obtiene lista de los todos los usuarios y lo devuelve en una respuesta HTTP
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ObtenerTodosUsuariosAsync()
        {
            var usuarios = await _usuariosService.ObtenerTodosUsuariosAsync();
            return Ok(usuarios);
        }
        // Lo mismo que el anterior pero busca por documento
        [HttpGet("ObtenerUsuariosPorDni/{dni}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuarioPorDocumentoAsync(string dni)
        {
            var usuario = await _usuariosService.ObtenerUsuarioPorDocumentoAsync(dni);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet("clientes")]
        public async Task<ActionResult<List<Usuario>>> ObtenerClientes()
        {
            var clientes = await _usuariosService.ObtenerClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("administradores")]
        public async Task<ActionResult<List<Usuario>>> ObtenerAdministradores()
        {
            var administradores = await _usuariosService.ObtenerAdministradoresAsync();
            return Ok(administradores);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> RegistrarUsuarioAsync(Usuario usuario)
        {
            try
            {
                // Verificar el tipo de usuario seleccionado durante el registro
                var tipoUsuario = usuario.RolesUsuarios.tipo_rol;

                // Asignar automáticamente el rol según el tipo de usuario
                if (!string.IsNullOrEmpty(tipoUsuario))
                {
                    // Obtener el rol correspondiente desde la base de datos (o crearlo si no existe)
                    var rol = await _rolesService.ObtenerRolPorTipoAsync(tipoUsuario);

                    if (rol == null)
                    {
                        rol = new Rol { tipo_rol = tipoUsuario };
                        await _rolesService.CrearRolAsync(rol); // Crea el rol en la base de datos
                    }

                    usuario.RolesUsuarios = rol;
                }

                // Crear el usuario sin cifrar la contraseña
                var usuarioCreado = await _usuariosService.RegistrarUsuarioAsync(usuario, tipoUsuario);

                return Ok(usuarioCreado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al registrar el usuario: {ex.Message}");
            }
        }

        [HttpPost("IniciarSesion")]
        public async Task<ActionResult<Usuario>> IniciarSesionAsync(string correo, string contraseña)
        {
            try
            {
                var usuario = await _usuariosService.IniciarSesionAsync(correo, contraseña);

                if (usuario == null)
                {
                    // El inicio de sesión falló
                    return NotFound("Credenciales de inicio de sesión incorrectas.");
                }

                // El inicio de sesión fue exitoso; puedes devolver el usuario o cualquier otro dato necesario
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al iniciar sesión: {ex.Message}");
            }
        }



        [HttpPut("{dni}")]
        public async Task<ActionResult<Usuario>> EditarUsuarioAsync(int dni, Usuario usuario)
        {
            string dniStr = dni.ToString();

            if (dniStr != usuario.dni)
            {
                return BadRequest("Los DNI no coinciden");
            }

            try
            {
                // Actualizar el rol según el tipo de usuario
                var tipoUsuario = usuario.RolesUsuarios?.tipo_rol;

                if (!string.IsNullOrEmpty(tipoUsuario))
                {
                    // Obtener el rol correspondiente desde la base de datos (o crearlo si no existe)
                    var rol = await _rolesService.ObtenerRolPorTipoAsync(tipoUsuario);

                    if (rol == null)
                    {
                        rol = new Rol { tipo_rol = tipoUsuario };
                        await _rolesService.CrearRolAsync(rol); // Crea el rol en la base de datos
                    }

                    usuario.RolesUsuarios = rol;
                }

                // Verificar si se proporcionó una nueva contraseña y realizar el hashing si es necesario
                if (!string.IsNullOrEmpty(usuario.contraseña))
                {
                    using (var sha256 = SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(usuario.contraseña));
                        usuario.contraseña = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    }
                }

                var usuarioEditado = await _usuariosService.EditarUsuarioAsync(dni, usuario);
                return Ok(usuarioEditado);
            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, $"Error al registrar el usuario: {ex.Message}");
                //return NotFound(ex.Message);
                return NotFound($"Usuario con DNI {dniStr} no encontrado");
            }
        }

        // Lo mismo que los anteriores pero esta vez los elimina
        [HttpDelete("{dni}")]
        public async Task<IActionResult> EliminarUsuarioAsync(string dni)
        {
            try
            {
                await _usuariosService.EliminarUsuarioAsync(dni);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}

