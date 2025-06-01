namespace LojaRoupas.Models
{
    //Aqui temos o model de contratos que pode ser ajustado de acordo com a necessidade do cliente ou do .csv que ele nos enviar
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
