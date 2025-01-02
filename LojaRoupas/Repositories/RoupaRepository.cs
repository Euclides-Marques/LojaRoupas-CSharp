using LojaRoupas.Context;
using LojaRoupas.Models;
using LojaRoupas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaRoupas.Repositories
{
    public class RoupaRepository : IRoupaRepository
    {
        private readonly AppDbContext _context;

        public RoupaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Roupa> Roupas => _context.Roupas.Include(c => c.Categoria).Include(m => m.Marca);

        public Roupa GetRoupaById(int roupaId)
        {
            return _context.Roupas.FirstOrDefault(r => r.Id.Equals(roupaId));
        }
    }
}
