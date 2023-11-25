using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI.Modelos;

namespace ViajePlusBDAPI.Servicios
{
    public class UnidadTransporteService : IUnidadTransporteService
    {
        private readonly MiDbContext _context;

        public UnidadTransporteService(MiDbContext context)
        {
            _context = context;
        }

        public async Task<List<UnidadTransporte>> ObtenerTodasUnidadesTransporteAsync()
        {
            return await _context.UnidadesTransporte.ToListAsync();
        }

        public async Task<UnidadTransporte> ObtenerUnidadTransportePorIdAsync(int id)
        {
            return await _context.UnidadesTransporte.FindAsync(id);
        }

        public async Task<UnidadTransporte> AgregarUnidadTransporteAsync(UnidadTransporte unidadTransporte)
        {
            _context.UnidadesTransporte.Add(unidadTransporte);
            await _context.SaveChangesAsync();
            return unidadTransporte;
        }

        public async Task<UnidadTransporte> EditarUnidadTransporteAsync(int id, UnidadTransporte unidadTransporte)
        {
            var unidadExistente = await _context.UnidadesTransporte.FindAsync(id);

            if (unidadExistente == null)
            {
                throw new Exception("Unidad de transporte no encontrada");
            }

            unidadExistente.tipo_unidad = unidadTransporte.tipo_unidad;
            unidadExistente.categoria = unidadTransporte.categoria;
            unidadExistente.asientos = unidadTransporte.asientos;

            await _context.SaveChangesAsync();

            return unidadExistente;
        }

        public async Task EliminarUnidadTransporteAsync(int id)
        {
            var unidadExistente = await _context.UnidadesTransporte.FindAsync(id);

            if (unidadExistente == null)
            {
                throw new Exception("Unidad de transporte no encontrada");
            }

            _context.UnidadesTransporte.Remove(unidadExistente);
            await _context.SaveChangesAsync();
        }
    }

    public interface IUnidadTransporteService
    {
        Task<List<UnidadTransporte>> ObtenerTodasUnidadesTransporteAsync();
        Task<UnidadTransporte> ObtenerUnidadTransportePorIdAsync(int id);
        Task<UnidadTransporte> AgregarUnidadTransporteAsync(UnidadTransporte unidadTransporte);
        Task<UnidadTransporte> EditarUnidadTransporteAsync(int id, UnidadTransporte unidadTransporte);
        Task EliminarUnidadTransporteAsync(int id);
    }
}

