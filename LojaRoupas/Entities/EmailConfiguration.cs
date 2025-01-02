namespace LojaRoupas.Entities
{
    public class EmailConfiguration
    {
        public string NomeRemetente { get; set; }
        public string EmailRemetente { get; set; }
        public string Senha { get; set; }
        public string EnderecoServicoEmail { get; set; }
        public int PortaServidorEmail { get; set; }
        public bool UsaSsl { get; set; }
    }
}
