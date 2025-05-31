using Microsoft.AspNetCore.Identity;

namespace LojaRoupas.Models
{
    //Aqui é onde eu criei minhas classes que serão minhas tabelas e as suas instâncias que serão os atributos da classe, aqui podemos ver que o nome que eu coloquei,
    //será o nome que eu poderei usar no código e como ele será mostrado no banco, mas eu tbm poderia ter usado o [Column("nome_que_eu_quero_no_banco")] através do DataAnnotations
    //E também foi aqui que eu coloquei uma coluna a mais na tabela de AspNetUsers
    public class ApplicationUser : IdentityUser
    {
        public Guid? ClienteId { get; set; }
    }
}
