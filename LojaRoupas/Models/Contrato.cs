namespace LojaRoupas.Models
{
    public class Contratos
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string NumeroContrato { get; set; }
        public string Produto { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
        public string UsuarioImportador { get; set; }
        public DateTime DataImportacao { get; set; }
    }
}
