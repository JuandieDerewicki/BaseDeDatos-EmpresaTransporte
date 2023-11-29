using Microsoft.EntityFrameworkCore;
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

        public async Task<Servicio_Usuario> EditarServicioUsuarioAsync(int id, Servicio_Usuario servicioUsuario)
        {
            var servicioUsuarioExistente = await _context.Servicios_Usuarios.FirstOrDefaultAsync(su => su.id == id);

            if (servicioUsuarioExistente == null)
            {
                throw new Exception("Servicio de usuario no encontrado");
            }

            // Realizar las actualizaciones necesarias en servicioUsuarioExistente con los datos de servicioUsuario

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


        //public async Task<Servicio_Usuario> AgregarServicioUsuarioYReservaAsync(Servicio_Usuario servicioUsuario)
        //{
        //    int? disponibilidadAntes = null;
        //    int? disponibilidadDespues = null;
        //    double? costoFinalAntes = null;
        //    double? costoFinalDespues = null;

        //    try
        //    {
        //        if (servicioUsuario == null)
        //        {
        //            throw new ArgumentNullException(nameof(servicioUsuario), "El servicioUsuario no puede ser nulo.");
        //        }

        //        // Verificar si existe el usuario
        //        if (string.IsNullOrEmpty(servicioUsuario.dni_usuario))
        //        {
        //            throw new Exception("El dni_usuario no puede estar vacío.");
        //        }

        //        var usuario = await ObtenerUsuarioAsync(servicioUsuario.dni_usuario);

        //        if (usuario == null)
        //        {
        //            throw new Exception("El usuario no existe");
        //        }

        //        servicioUsuario.Usuario = usuario;

        //        // Obtener la disponibilidad antes de la reserva
        //        //disponibilidadAntes = await ObtenerDisponibilidadAntesReservaAsync(servicioUsuario.id_servicio ?? 0);

        //        // Obtener el costo final antes de la reserva
        //        costoFinalAntes = await ObtenerCostoFinalAntesReservaAsync(servicioUsuario.id_servicio ?? 0);

        //        // Obtener el servicio asociado
        //        var servicio = await ObtenerServicioAsync(servicioUsuario.id_servicio ?? 0);

        //        if (servicio == null)
        //        {
        //            throw new Exception("El servicio no existe");
        //        }

        //        servicioUsuario.Servicio = servicio;

        //        // Obtener el punto intermedio asociado
        //        if (servicioUsuario.id_puntoIntermedio.HasValue)
        //        {
        //            var puntoIntermedio = await ObtenerPuntoIntermedioAsync(servicioUsuario.id_puntoIntermedio.Value);

        //            if (puntoIntermedio == null)
        //            {
        //                throw new Exception("El punto intermedio no existe");
        //            }

        //            servicioUsuario.PuntoIntermedio = puntoIntermedio;
        //        }

        //        // Calcular el costo final
        //        servicioUsuario.CalcularCostoFinal(servicioUsuario.Servicio);

        //        // Verificar la disponibilidad del servicio si es necesario
        //        if (servicioUsuario.id_servicio.HasValue)
        //        {
        //            // Considera agregar una verificación más detallada de la disponibilidad aquí según tu lógica de negocio
        //            await VerificarDisponibilidadAsync(servicioUsuario.id_servicio.Value);

        //            // Obtener la disponibilidad después de la reserva
        //            disponibilidadDespues = await ObtenerDisponibilidadDespuesReservaAsync(servicioUsuario.id_servicio ?? 0);

        //            // Obtener el costo final después de la reserva
        //            costoFinalDespues = await ObtenerCostoFinalDespuesReservaAsync(servicioUsuario.id_servicio ?? 0);
        //        }

        //        // Agregar el servicioUsuario al contexto y guardar los cambios en la base de datos
        //        _context.Servicios_Usuarios.Add(servicioUsuario);
        //        await _context.SaveChangesAsync();

        //        // Incluir disponibilidad y costo final en la respuesta
        //        var response = new
        //        {
        //            id = servicioUsuario.id,
        //            dni_usuario = servicioUsuario.dni_usuario,
        //            id_servicio = servicioUsuario.id_servicio,
        //            id_puntoIntermedio = servicioUsuario.id_puntoIntermedio,
        //            tipo_atencion = servicioUsuario.tipo_atencion,
        //            costo_final = servicioUsuario.costo_final,
        //            disponibilidad_antes = disponibilidadAntes,
        //            disponibilidad_despues = disponibilidadDespues,
        //            costo_final_antes = costoFinalAntes,
        //            costo_final_despues = costoFinalDespues,
        //            // Otros campos que desees incluir en la respuesta
        //        };

        //        return servicioUsuario;
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        throw new InvalidOperationException($"Error al agregar el servicioUsuario: {ex.ParamName} - {ex.Message}", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException($"Error al agregar el servicioUsuario: {ex.Message}", ex);
        //    }
        //}

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

                // Calcular el costo final antes de la reserva
                double costoFinalAntes = await ObtenerCostoFinalAsync(servicioUsuario);

                // Calcular el costo final
                ObtenerCostoFinalAsync(servicioUsuario); /////// CAMBIAR ESTA PARTE

                // Verificar la disponibilidad después de la reserva
                int? disponibilidadDespues = await ObtenerDisponibilidadAsync(servicio.id_servicio);

                // Obtener el costo final después de la reserva
                double costoFinalDespues = await ObtenerCostoFinalAsync(servicioUsuario);

                // Agregar el servicioUsuario al contexto y guardar los cambios en la base de datos
                _context.Servicios_Usuarios.Add(servicioUsuario);
                await _context.SaveChangesAsync();

                // Incluir disponibilidad y costo final en la respuesta
                var response = new
                {
                    id = servicioUsuario.id,
                    dni_usuario = servicioUsuario.dni_usuario,
                    id_servicio = servicioUsuario.id_servicio,
                    id_puntoIntermedio = servicioUsuario.id_puntoIntermedio,
                    tipo_atencion = servicioUsuario.tipo_atencion,
                    costo_final = servicioUsuario.costo_final,
                    disponibilidad_antes = disponibilidadAntes,
                    disponibilidad_despues = disponibilidadDespues,
                    costo_final_antes = costoFinalAntes,
                    costo_final_despues = costoFinalDespues,
                    // Otros campos que desees incluir en la respuesta
                };

                return servicioUsuario;
                // Puedes devolver la respuesta o simplemente no devolver nada (depende de tu diseño)
            }
            catch (ArgumentNullException ex)
            {
                throw new InvalidOperationException($"Error al reservar el pasaje: {ex.ParamName} - {ex.Message}", ex);
            }
            catch (Exception ex)
            {
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
                costoPredeterminado *= 1.20; // Aumento del 20% para atención ejecutiva
            }

            // Obtener la categoría de la unidad de transporte asociada al servicio
            string categoriaTransporte = servicioUsuario.Servicio.UnidadTransporte?.categoria ?? "";

            // Aplicar el aumento por categoría de la unidad de transporte
            switch (categoriaTransporte)
            {
                case "Comun":
                    // No hay aumento
                    break;
                case "Semicama":
                    costoPredeterminado *= 1.10; // Aumento del 10% para categoría semicama
                    break;
                case "Cochecama":
                    costoPredeterminado *= 1.30; // Aumento del 30% para categoría cochecama
                    break;
                    // Puedes agregar más casos según las categorías que tengas
            }

            // Verificar si hay un punto intermedio asociado
            if (servicioUsuario.PuntoIntermedio == null)
            {
                // No hay punto intermedio, servicio completo, no hay descuento
            }
            else
            {
                // Hay un punto intermedio, se aplica un descuento del 10%
                costoPredeterminado *= 0.90;
            }

            return costoPredeterminado;
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
                .FirstOrDefaultAsync(u => u.dni == dniUsuario);
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

            // Elimina la reserva de la base de datos
            _context.Servicios_Usuarios.Remove(reserva);
            await _context.SaveChangesAsync();

            // Incrementa la disponibilidad de pasajes
            var servicio = await _context.Servicios.FindAsync(reserva.id_servicio);
            if (servicio != null)
            {
                //servicio.disponibilidad++;
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
        Task<Servicio_Usuario> EditarServicioUsuarioAsync(int id, Servicio_Usuario servicioUsuario);
        Task EliminarServicioUsuarioAsync(int id);
        Task CancelarReservaAsync(int reservaId);
        //Task<Servicio_Usuario> AgregarServicioUsuarioYReservaAsync(Servicio_Usuario servicioUsuario);
        Task<Servicio_Usuario> ReservaPasajeAsync(Servicio_Usuario servicioUsuario);
        Task<Usuario?> ObtenerUsuarioAsync(string dniUsuario);
        Task<Servicio?> ObtenerServicioAsync(int idServicio);
        Task<PuntoIntermedio?> ObtenerPuntoIntermedioAsync(int idPuntoIntermedio);
        Task<int?> ObtenerDisponibilidadAntesReservaAsync(int idServicio);
        Task<int?> ObtenerDisponibilidadDespuesReservaAsync(int idServicio);

        Task<int?> ObtenerDisponibilidadAsync(int idServicio);


        }
}
