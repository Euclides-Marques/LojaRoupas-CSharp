using LojaRoupas.Context;
using LojaRoupas.Models;
using LojaRoupas.Repositories.Interfaces;

namespace LojaRoupas.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly AppDbContext _context;

        public MarcaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Marca> Marcas => _context.Marcas;
    }
}
