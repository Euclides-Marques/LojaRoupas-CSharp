using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaRoupas.Models
{
    //Aqui é onde eu criei minhas classes que serão minhas tabelas e as suas instâncias que serão os atributos da classe, aqui podemos ver que o nome que eu coloquei,
    //será o nome que eu poderei usar no código e como ele será mostrado no banco, mas eu tbm poderia ter usado o [Column("nome_que_eu_quero_no_banco")] através do DataAnnotations
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
        public string? ImagemUrl { get; set; }
        public string Genero { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataDeCadastro { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey("Categoria")]
        public Guid CategoriaId { get; set; }

        [ForeignKey("Marca")]
        public Guid MarcaId { get; set; }
        public bool Favorito { get; set; }
        public bool AdicionarCarrinho { get; set; }
        public bool Lancamento { get; set; }
        public bool Promocao { get; set; }
        public decimal PrecoPromocao { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Marca Marca { get; set; }
    }
}
