using LojaRoupas.Models;

namespace LojaRoupas.Repositories.Interfaces
{
    public interface IRoupaRepository
    {
        IEnumerable<Roupa> Roupas { get; }
        Roupa GetRoupaById(int roupaId);
    }
}
