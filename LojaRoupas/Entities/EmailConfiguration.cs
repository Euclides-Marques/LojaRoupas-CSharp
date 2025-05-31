namespace LojaRoupas.Entities
{
    //Aqui é onde eu criei minha classe que está sendo a minha entidade de configuração do e-mail
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
