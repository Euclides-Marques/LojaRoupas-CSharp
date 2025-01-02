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
    }
}
