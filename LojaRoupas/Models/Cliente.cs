using System.ComponentModel.DataAnnotations;

namespace LojaRoupas.Models
{
    //Aqui é onde eu criei minhas classes que serão minhas tabelas e as suas instâncias que serão os atributos da classe, aqui podemos ver que o nome que eu coloquei,
    //será o nome que eu poderei usar no código e como ele será mostrado no banco, mas eu tbm poderia ter usado o [Column("nome_que_eu_quero_no_banco")] através do DataAnnotations
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInclusao { get; set; }
    }
}
