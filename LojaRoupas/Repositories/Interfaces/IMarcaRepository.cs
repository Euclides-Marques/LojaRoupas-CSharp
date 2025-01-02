using LojaRoupas.Models;

namespace LojaRoupas.Repositories.Interfaces
{
    public interface IMarcaRepository
    {
        IEnumerable<Marca> Marcas { get; }
    }
}
