namespace LojaRoupas.Models
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
