using LojaRoupas.Models;

namespace LojaRoupas.Repositories.Interfaces
{
    //Aqui eu criei interfaces que podem ser chamadas no controller e aqui liga para os repositories que é de onde será herdado as informações
    public interface IRoupaRepository
    {
        IEnumerable<Roupa> Roupas { get; }
        Roupa GetRoupaById(int roupaId);
    }
}
