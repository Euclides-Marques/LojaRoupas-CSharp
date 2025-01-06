using LojaRoupas.Models;
using LojaRoupas.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace LojaRoupas.Controllers
{
    public class RecuperarSenhaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public RecuperarSenhaController(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // Exibe o formulário para recuperação de senha
        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        // Método POST para enviar o e-mail com o link de redefinição de senha
        [HttpPost]
        public async Task<IActionResult> RecuperarSenha(string email)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(email);
                if (usuario != null)
                {
                    // Gerar o token de redefinição de senha
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

                    // Gerar a URL para redefinir a senha, passando o token e o e-mail
                    var urlResetarSenha = Url.Action("RedefinirSenha", "RecuperarSenha", new { token, email = usuario.Email }, Request.Scheme);

                    // Enviar o e-mail com o link para redefinir a senha
                    var msg = new StringBuilder();
                    msg.Append("<h1>Recuperação de Senha</h1>");
                    msg.Append($"<p>Clique <a href='{urlResetarSenha}'>aqui</a> para redefinir sua senha.</p>");
                    await _emailSender.SendEmailAsync(usuario.Email, "Recuperação de Senha", "", msg.ToString());

                    // Passa o e-mail e token para o modelo de redefinir senha
                    var model = new RedefinirSenhaModel
                    {
                        Email = usuario.Email,
                        Token = token
                    };

                    // Redireciona para a página de "E-mail de Recuperação Enviado"
                    return RedirectToAction("EmailRecuperacaoEnviado", model);
                }
            }

            return View();
        }

        // Exibe a página informando que o e-mail foi enviado
        [HttpGet]
        public IActionResult EmailRecuperacaoEnviado()
        {
            return View();
        }

        // Exibe o formulário para redefinir a senha
        [HttpGet]
        public IActionResult RedefinirSenha(string token, string email)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Um token deve ser fornecido para redefinir a senha.");
            }

            // Passa o token e o e-mail para o modelo
            var model = new RedefinirSenhaModel
            {
                Token = token,
                Email = email
            };

            return View(model);
        }

        // Método POST para redefinir a senha
        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(RedefinirSenhaModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByEmailAsync(model.Email);
                if (usuario != null)
                {
                    // Usar o token gerado sem alterações para resetar a senha
                    var resultado = await _userManager.ResetPasswordAsync(usuario, model.Token, model.Senha);
                    if (resultado.Succeeded)
                    {
                        // Redireciona para a página de "Senha Redefinida"
                        return RedirectToAction("SenhaRedefinida");
                    }

                    // Se houve erro ao tentar redefinir a senha, mostra os erros
                    foreach (var erro in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, erro.Description);
                    }
                }
            }

            return View(model);
        }

        // Exibe a página de confirmação de senha redefinida
        [HttpGet]
        public IActionResult SenhaRedefinida()
        {
            return View();
        }
    }

    // Modelo para passar os dados necessários para redefinir a senha
    public class RedefinirSenhaModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public string Token { get; set; }
    }
}
