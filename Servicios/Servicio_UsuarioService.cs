using Microsoft.EntityFrameworkCore;
using System.Globalization;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class Servicio_UsuarioService : IServicioUsuarioService
    {
        private readonly MiDbContext _context;

        public Servicio_UsuarioService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Servicio_Usuario>> ObtenerTodosServiciosUsuariosAsync()
        {
            return await _context.Servicios_Usuarios.ToListAsync();
        }

        public async Task<Servicio_Usuario> ObtenerServicioUsuarioPorIdAsync(int id)
        {
            return await _context.Servicios_Usuarios.FirstOrDefaultAsync(su => su.id == id);
        }

        public async Task<List<Servicio_Usuario>> ObtenerServiciosUsuarioPorDniAsync(string dniUsuario)
        {
            return await _context.Servicios_Usuarios.Where(su => su.dni_usuario == dniUsuario).ToListAsync();
        }

        public async Task<List<Servicio_Usuario>> ObtenerServiciosUsuarioPorServicioAsync(int idservicio)
        {
            return await _context.Servicios_Usuarios.Where(su => su.id_servicio == idservicio).ToListAsync();
        }

        public async Task<List<Servicio_Usuario>> ObtenerServiciosVentaTrueAsync()
        {
            return await _context.Servicios_Usuarios.Where(su => su.venta).ToListAsync();
        }

        public async Task<List<Servicio_Usuario>> ObtenerServiciosReservadosAsync()
        {
            return await _context.Servicios_Usuarios.Where(su => !su.venta).ToListAsync();
        }

        // ESTADISTICAS 
        public async Task<List<Servicio_Usuario>> ObtenerPasajesVendidosCamaAsync()
        {
            return await _context.Servicios_Usuarios
                .Where(su => su.venta && su.Servicio.UnidadTransporte.categoria == "Cochecama")
                .ToListAsync();
        }

        public async Task<List<Servicio_Usuario>> ObtenerPasajesVendidosSemicamaAsync()
        {
            return await _context.Servicios_Usuarios
                .Where(su => su.venta && su.Servicio.UnidadTransporte.categoria == "Semicama")
                .ToListAsync();
        }

        public async Task<List<Servicio_Usuario>> ObtenerPasajesVendidosComunAsync()
        {
            return await _context.Servicios_Usuarios
                .Where(su => su.venta && su.Servicio.UnidadTransporte.categoria == "Comun")
                .ToListAsync();
        }

        public async Task<List<Servicio_Usuario>> ObtenerPasajesVendidosConItinerarioAsync()
        {
            return await _context.Servicios_Usuarios
                .Include(su => su.Servicio)
                .ThenInclude(s => s.Itinerario)
                .Where(su => su.venta == false)
                .ToListAsync();
        }


        public async Task<Servicio_Usuario> EditarServicioUsuarioAsync(int id, Servicio_Usuario servicioUsuario)
        {
            var servicioUsuarioExistente = await _context.Servicios_Usuarios.FirstOrDefaultAsync(su => su.id == id);

            if (servicioUsuarioExistente == null)
            {
                throw new Exception("Servicio de usuario no encontrado");
            }

            // Realizar las actualizaciones necesarias en servicioUsuarioExistente con los datos de servicioUsuario
            servicioUsuarioExistente.venta = servicioUsuario.venta;

            await _context.SaveChangesAsync();
            return servicioUsuarioExistente;
        }

        public async Task EliminarServicioUsuarioAsync(int id)
        {
            var servicioUsuarioExistente = await _context.Servicios_Usuarios.FirstOrDefaultAsync(su => su.id == id);

            if (servicioUsuarioExistente == null)
            {
                throw new Exception("Servicio de usuario no encontrado");
            }

            _context.Servicios_Usuarios.Remove(servicioUsuarioExistente);
            await _context.SaveChangesAsync();
        }


        public async Task<Servicio_Usuario> ReservaPasajeAsync(Servicio_Usuario servicioUsuario)
        {
            try
            {
                if (servicioUsuario == null)
                {
                    throw new ArgumentNullException(nameof(servicioUsuario), "El servicioUsuario no puede ser nulo.");
                }

                // Verificar si existe el usuario
                if (string.IsNullOrEmpty(servicioUsuario.dni_usuario))
                {
                    throw new Exception("El dni_usuario no puede estar vacío.");
                }

                var usuario = await ObtenerUsuarioAsync(servicioUsuario.dni_usuario);

                if (usuario == null)
                {
                    throw new Exception("El usuario no existe");
                }

                servicioUsuario.Usuario = usuario;

                // Obtener el servicio asociado
                var servicio = await ObtenerServicioAsync(servicioUsuario.id_servicio ?? 0);

                if (servicio == null)
                {
                    throw new Exception("El servicio no existe");
                }

                servicioUsuario.Servicio = servicio;

                // Obtener el punto intermedio asociado
                if (servicioUsuario.id_puntoIntermedio.HasValue)
                {
                    var puntoIntermedio = await ObtenerPuntoIntermedioAsync(servicioUsuario.id_puntoIntermedio.Value);

                    if (puntoIntermedio == null)
                    {
                        throw new Exception("El punto intermedio no existe");
                    }

                    servicioUsuario.PuntoIntermedio = puntoIntermedio;
                }

                // Verificar la disponibilidad del servicio antes de la reserva
                await VerificarDisponibilidadAsync(servicio.id_servicio);

                // Obtener la disponibilidad antes de la reserva
                int? disponibilidadAntes = await ObtenerDisponibilidadAsync(servicio.id_servicio);

                // Verificar que no sea el mismo día de la partida antes de la reserva
                await VerificarReservaMismoDiaAsync(servicio.id_itinerario ?? 0);

                // Calcular el costo final antes de la reserva
                double costoFinal = await ObtenerCostoFinalAsync(servicioUsuario);

                // Asignar el costo final calculado al servicioUsuario
                servicioUsuario.costo_final = costoFinal;

                // Verificar la disponibilidad después de la reserva
                int? disponibilidadDespues = await ObtenerDisponibilidadAsync(servicio.id_servicio);

                // Agregar el servicioUsuario al contexto y guardar los cambios en la base de datos
                _context.Servicios_Usuarios.Add(servicioUsuario);
                await _context.SaveChangesAsync();

                return servicioUsuario;
                // Puedes devolver la respuesta o simplemente no devolver nada (depende de tu diseño)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción interna: {ex.InnerException?.Message}");
                Console.WriteLine($"StackTrace interna: {ex.InnerException?.StackTrace}");
                throw new InvalidOperationException($"Error al reservar el pasaje: {ex.Message}", ex);
            }
        }

        private async Task<double> ObtenerCostoFinalAsync(Servicio_Usuario servicioUsuario)
        {
            // Obtener el costo predeterminado del servicio
            double costoPredeterminado = servicioUsuario.Servicio.costo_predeterminado ?? 0;

            // Aplicar el aumento por tipo de atención
            if (servicioUsuario.tipo_atencion == "Ejecutivo")
            {
                costoPredeterminado = costoPredeterminado + (costoPredeterminado * 1.20); // Aumento del 20% para atención ejecutiva
            }

            //string categoriaTransporte = "";
            //if (servicioUsuario.Servicio != null && servicioUsuario.Servicio.UnidadTransporte != null)
            //{
                //categoriaTransporte = servicioUsuario.Servicio.UnidadTransporte.categoria?.Trim(); 
                //categoriaTransporte = servicioUsuario.Servicio.UnidadTransporte.categoria;
            var servicio = _context.Servicios.Find(servicioUsuario.id_servicio);
            var categoriaTransporte = _context.UnidadesTransporte.Find(servicio.id_unidadTransporte);
            if (string.Compare(categoriaTransporte.categoria, "Cochecama", StringComparison.OrdinalIgnoreCase) == 0)
            {
                costoPredeterminado = costoPredeterminado + (costoPredeterminado * 1.30);
            }
            else if (string.Compare(categoriaTransporte.categoria, "Semicama", StringComparison.OrdinalIgnoreCase) == 0)
            {
                costoPredeterminado = costoPredeterminado + (costoPredeterminado * 1.10);
            }
            //}



            // Aplicar el aumento por categoría de la unidad de transporte
            //if (!string.IsNullOrEmpty(categoriaTransporte))
            //{
            //    if (categoriaTransporte == "Semicama")
            //    {
            //        costoPredeterminado = costoPredeterminado + (costoPredeterminado * 1.10); // Aumento del 10% para categoría semicama
            //    }
            //    else if (categoriaTransporte == "Cochecama")
            //    {
            //        costoPredeterminado = costoPredeterminado + (costoPredeterminado * 1.30); // Aumento del 30% para categoría cochecama
            //    }
            //}

            //// Obtener la categoría de la unidad de transporte asociada al servicio
            //string categoriaTransporte = servicioUsuario.Servicio.UnidadTransporte?.categoria ?? "";

            //// Aplicar el aumento por categoría de la unidad de transporte
            //switch (categoriaTransporte)
            //{
            //    case "Comun":
            //        // No hay aumento
            //        break;
            //    case "Semicama":
            //        costoPredeterminado *= 1.10; // Aumento del 10% para categoría semicama
            //        break;
            //    case "Cochecama":
            //        costoPredeterminado *= 1.30; // Aumento del 30% para categoría cochecama
            //        break;
            //        // Puedes agregar más casos según las categorías que tengas
            //}

            // Verificar si hay un punto intermedio asociado
            if (servicioUsuario.PuntoIntermedio != null)
            {
                // Hay un punto intermedio, restar el 10%
                costoPredeterminado = costoPredeterminado + (costoPredeterminado * 0.90);
            }

            servicioUsuario.costo_final = costoPredeterminado;
            return servicioUsuario.costo_final ?? 0;
        }

        private async Task VerificarReservaMismoDiaAsync(int idItinerario)
        {
            // Obtener la fecha de partida del itinerario
            var fechaPartidaItinerario = await _context.Itinerarios
                .Where(i => i.id_itinerario == idItinerario)
                .Select(i => i.fecha_partida)
                .FirstOrDefaultAsync();

            if (fechaPartidaItinerario == null || !DateTime.TryParseExact(fechaPartidaItinerario, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaPartida))
            {
                throw new InvalidOperationException("No se puede verificar la reserva en el mismo día. Error al obtener la fecha de partida del itinerario.");
            }

            // Verificar si la reserva es en el mismo día que la fecha de partida
            if (fechaPartida.Date == DateTime.Now.Date)
            {
                throw new InvalidOperationException("No se puede realizar la reserva en el mismo día de la partida del itinerario.");
            }
        }

        // Metodos para obtenciones
        public async Task<int?> ObtenerDisponibilidadAntesReservaAsync(int idServicio)
        {
            var servicio = await ObtenerServicioAsync(idServicio);

            if (servicio == null)
            {
                throw new Exception("El servicio no existe");
            }

            return servicio.disponibilidad;
        }

        public async Task<int?> ObtenerDisponibilidadDespuesReservaAsync(int idServicio)
        {
            return await ObtenerDisponibilidadAsync(idServicio);
        }

        public async Task<Servicio?> ObtenerServicioAsync(int idServicio)
        {
            return await _context.Servicios
                .FirstOrDefaultAsync(s => s.id_servicio == idServicio);
        }

        public async Task<PuntoIntermedio?> ObtenerPuntoIntermedioAsync(int idPuntoIntermedio)
        {
            return await _context.PuntosIntermedios
                .FirstOrDefaultAsync(p => p.id_puntoIntermedio == idPuntoIntermedio);
        }

        public async Task<Usuario?> ObtenerUsuarioAsync(string dniUsuario)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.id_dni == dniUsuario);
        }

        public async Task<int?> ObtenerDisponibilidadAsync(int idServicio)
        {
            var servicio = await _context.Servicios
                .Where(s => s.id_servicio == idServicio)
                .FirstOrDefaultAsync();

            return servicio.disponibilidad;
        }


        private async Task VerificarDisponibilidadAsync(int idServicio)
        {
            var servicio = await _context.Servicios.FindAsync(idServicio);

            if (servicio == null)
            {
                // El servicio no existe
                throw new InvalidOperationException("El servicio no existe.");
            }

            if (servicio.disponibilidad <= 0)
            {
                // No hay disponibilidad de pasajes
                throw new InvalidOperationException("No hay disponibilidad de pasajes para este servicio.");
            }

            // Actualiza la disponibilidad en base a la cantidad de asientos de la unidad de transporte
            servicio.disponibilidad--;
            _context.Servicios.Update(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task CancelarReservaAsync(int reservaId)
        {
            var reserva = await _context.Servicios_Usuarios.FindAsync(reservaId);

            if (reserva == null)
            {
                // La reserva no existe
                throw new InvalidOperationException("La reserva no existe.");
            }

            // Verificar si se puede cancelar la reserva en el mismo día
            await VerificarReservaMismoDiaAsync(reserva.id_servicio ?? 0);

            // Elimina la reserva de la base de datos
            _context.Servicios_Usuarios.Remove(reserva);
            await _context.SaveChangesAsync();

            // Incrementa la disponibilidad de pasajes
            var servicio = await _context.Servicios.FindAsync(reserva.id_servicio);
            if (servicio != null)
            {
                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();
            }
        }


    }

    public interface IServicioUsuarioService
    {
        Task<List<Servicio_Usuario>> ObtenerTodosServiciosUsuariosAsync();
        Task<Servicio_Usuario> ObtenerServicioUsuarioPorIdAsync(int id);
        Task<List<Servicio_Usuario>> ObtenerServiciosUsuarioPorDniAsync(string dniUsuario);
        Task<List<Servicio_Usuario>> ObtenerServiciosUsuarioPorServicioAsync(int idservicio);
        Task<List<Servicio_Usuario>> ObtenerServiciosVentaTrueAsync();
        Task<List<Servicio_Usuario>> ObtenerServiciosReservadosAsync();
        // ESTADISTICAS
        Task<List<Servicio_Usuario>> ObtenerPasajesVendidosCamaAsync();
        Task<List<Servicio_Usuario>> ObtenerPasajesVendidosSemicamaAsync();

        Task<List<Servicio_Usuario>> ObtenerPasajesVendidosComunAsync();
        Task<List<Servicio_Usuario>> ObtenerPasajesVendidosConItinerarioAsync();


        // ESTADISTICAS
        Task<Servicio_Usuario> EditarServicioUsuarioAsync(int id, Servicio_Usuario servicioUsuario);
        Task EliminarServicioUsuarioAsync(int id);
        Task CancelarReservaAsync(int reservaId);
        Task<Servicio_Usuario> ReservaPasajeAsync(Servicio_Usuario servicioUsuario);
        Task<Usuario?> ObtenerUsuarioAsync(string dniUsuario);
        Task<Servicio?> ObtenerServicioAsync(int idServicio);
        Task<PuntoIntermedio?> ObtenerPuntoIntermedioAsync(int idPuntoIntermedio);
        Task<int?> ObtenerDisponibilidadAntesReservaAsync(int idServicio);
        Task<int?> ObtenerDisponibilidadDespuesReservaAsync(int idServicio);

        Task<int?> ObtenerDisponibilidadAsync(int idServicio);


        }
}
