using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class ItinerarioService : IItinerarioService
    {

        private readonly MiDbContext _context;

        public ItinerarioService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Itinerario>> ObtenerTodosItinerariosAsync()
        {
            return await _context.Itinerarios.ToListAsync();
        }

        public async Task<Itinerario> ObtenerItinerarioPorIdAsync(int id)
        {
            return await _context.Itinerarios.FindAsync(id);
        }

        public async Task<List<Itinerario>> ObtenerItinerariosPorCiudadAsync(string ciudad)
        {
            return await _context.Itinerarios
                .Where(i => i.ciudad_origen == ciudad || i.ciudad_destino == ciudad)
                .ToListAsync();
        }

        public async Task<Itinerario> AgregarItinerarioAsync(Itinerario itinerario)
        {
            _context.Itinerarios.Add(itinerario);
            await _context.SaveChangesAsync();
            return itinerario;
        }

        public async Task<Itinerario> EditarItinerarioAsync(int id, Itinerario itinerario)
        {
            var itinerarioExistente = await _context.Itinerarios.FindAsync(id);

            if (itinerarioExistente == null)
            {
                throw new Exception("Itinerario no encontrado");
            }

            itinerarioExistente.ciudad_origen = itinerario.ciudad_origen;
            itinerarioExistente.ciudad_destino = itinerario.ciudad_destino;
            itinerarioExistente.fechaHora_partida = itinerario.fechaHora_partida;
            itinerarioExistente.fechaHora_llegada = itinerario.fechaHora_llegada;

            await _context.SaveChangesAsync();

            return itinerarioExistente;
        }

        public async Task EliminarItinerarioAsync(int id)
        {
            var itinerarioExistente = await _context.Itinerarios.FindAsync(id);

            if (itinerarioExistente == null)
            {
                throw new Exception("Itinerario no encontrado");
            }

            _context.Itinerarios.Remove(itinerarioExistente);
            await _context.SaveChangesAsync();
        }
    }

    public interface IItinerarioService
    {
        Task<List<Itinerario>> ObtenerTodosItinerariosAsync();
        Task<Itinerario> ObtenerItinerarioPorIdAsync(int id);
        Task<List<Itinerario>> ObtenerItinerariosPorCiudadAsync(string ciudad);
        Task<Itinerario> AgregarItinerarioAsync(Itinerario itinerario);
        Task<Itinerario> EditarItinerarioAsync(int id, Itinerario itinerario);
        Task EliminarItinerarioAsync(int id);
    }
}

