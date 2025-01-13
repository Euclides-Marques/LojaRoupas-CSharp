using System.ComponentModel.DataAnnotations;

namespace LojaRoupas.Models
{
    public class Marca
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataDeCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
