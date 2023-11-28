using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class ServicioService : IServicioService
    {
        private readonly MiDbContext _context;

        public ServicioService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Servicio>> ObtenerTodosServiciosAsync()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<Servicio> ObtenerServicioPorIdAsync(int id)
        {
            return await _context.Servicios.FirstOrDefaultAsync(s => s.id_servicio == id);
        }

        //public async Task<Servicio> AgregarServicioAsync(Servicio servicio)
        //{
        //    _context.Servicios.Add(servicio);
        //    await _context.SaveChangesAsync();
        //    return servicio;
        //}

        public async Task<Servicio> AgregarServicioAsync(Servicio servicio)
        {
            try
            {
                // Verifica si existe el itinerario y la unidad de transporte
                if (servicio.id_itinerario.HasValue)
                {
                    var itinerario = await ObtenerItinerarioAsync(servicio.id_itinerario.Value);

                    if (itinerario == null)
                    {
                        throw new Exception("El itinerario no existe");
                    }

                    servicio.Itinerario = itinerario;
                }

                if (servicio.id_unidadTransporte.HasValue)
                {
                    var unidadTransporte = await ObtenerUnidadTransporteAsync(servicio.id_unidadTransporte.Value);

                    if (unidadTransporte == null)
                    {
                        throw new Exception("La unidad de transporte no existe");
                    }

                    servicio.UnidadTransporte = unidadTransporte;
                }

                // Agrega el servicio al contexto
                _context.Servicios.Add(servicio);

                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();

                return servicio;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Lanza la excepción para que se maneje en el controlador
            }
        }

        public async Task<Itinerario> ObtenerItinerarioAsync(int itinerarioId)
        {
            return await _context.Itinerarios.FindAsync(itinerarioId);
        }

        public async Task<UnidadTransporte> ObtenerUnidadTransporteAsync(int unidadTransporteId)
        {
            return await _context.UnidadesTransporte.FindAsync(unidadTransporteId);
        }


        public async Task<Servicio> EditarServicioAsync(int id, Servicio servicio)
        {
            var servicioExistente = await _context.Servicios.FirstOrDefaultAsync(s => s.id_servicio == id);

            if (servicioExistente == null)
            {
                throw new Exception("Servicio no encontrado");
            }

            // Realizar las actualizaciones necesarias en el servicioExistente con los datos de servicio
            servicioExistente.costo_predeterminado = servicio.costo_predeterminado ?? servicioExistente.costo_predeterminado;
            servicioExistente.disponibilidad = servicio.disponibilidad;
            servicioExistente.id_itinerario = servicio.id_itinerario ?? servicioExistente.id_itinerario;
            servicioExistente.id_unidadTransporte = servicio.id_unidadTransporte ?? servicioExistente.id_unidadTransporte;

            await _context.SaveChangesAsync();
            return servicioExistente;
        }

        public async Task EliminarServicioAsync(int id)
        {
            var servicioExistente = await _context.Servicios.FirstOrDefaultAsync(s => s.id_servicio == id);

            if (servicioExistente == null)
            {
                throw new Exception("Servicio no encontrado");
            }

            _context.Servicios.Remove(servicioExistente);
            await _context.SaveChangesAsync();
        }
    }

    public interface IServicioService
    {
        Task<List<Servicio>> ObtenerTodosServiciosAsync();
        Task<Servicio> ObtenerServicioPorIdAsync(int id);
        //Task<List<Servicio>> ObtenerServiciosPorItinerarioAsync(int idItinerario);
        Task<Servicio> AgregarServicioAsync(Servicio servicio);
        Task<Servicio> EditarServicioAsync(int id, Servicio servicio);
        Task EliminarServicioAsync(int id);
        Task<Itinerario> ObtenerItinerarioAsync(int itinerarioId);

        Task<UnidadTransporte> ObtenerUnidadTransporteAsync(int unidadTransporteId);


    }
}
