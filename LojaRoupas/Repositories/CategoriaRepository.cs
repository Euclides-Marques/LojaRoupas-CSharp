using LojaRoupas.Context;
using LojaRoupas.Models;
using LojaRoupas.Repositories.Interfaces;

namespace LojaRoupas.Repositories
{
    //Aqui no meu repository de categoria eu falei que a classe irá herdar da interface, e chamará o _context que é onde estão feitos os meus model.
    //Isso fará com que ele acesse o nome dos models e as suas propriedades através dos nomes que eu dei para eles no projeto e caso eu mude como é para ser colocado no banco,
    //o que vai ser instanciado no código é o jeito que eu defini para o código em específico
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
