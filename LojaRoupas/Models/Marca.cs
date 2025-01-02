namespace LojaRoupas.Models
{
    public class Marca
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
