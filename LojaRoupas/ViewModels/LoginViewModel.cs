using LojaRoupas.Models;
using System.ComponentModel.DataAnnotations;

namespace LojaRoupas.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email Inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha Inválida")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        public string ReturnUrl { get; set; }

        public string CPF { get; set; }
        public string? Celular { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
    }
}
