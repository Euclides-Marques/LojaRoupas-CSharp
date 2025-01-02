using System.ComponentModel.DataAnnotations.Schema;

namespace LojaRoupas.Models
{
    public class Roupa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Tamanho { get; set; }
        public string Cor { get; set; }
        public string Material { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public string Genero { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey("Categoria")]
        public Guid CategoriaId { get; set; }

        [ForeignKey("Marca")]
        public Guid MarcaId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Marca Marca { get; set; }
    }
}
