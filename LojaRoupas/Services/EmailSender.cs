using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace LojaRoupas.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string assunto, string mensagemTexto, string mensagemHtml);
    }

    public class EmailSender : IEmailSender
    {
        private readonly string NomeRemetente = "Estilo de Torcedor";
        private readonly string EmailRemetente = "euclidesmarques270@gmail.com";
        private readonly string Senha = "yshzpewdtixgaody";
        private readonly string EnderecoServicoEmail = "smtp.gmail.com";
        private readonly int PortaServidorEmail = 465;
        private readonly bool UsaSsl = true;

        public EmailSender() { }

        public async Task SendEmailAsync(string email, string assunto, string mensagemTexto, string mensagemHtml)
        {
            var mensagem = new MimeMessage();
            var teste = NomeRemetente;
            var teste2 = EmailRemetente;
            mensagem.From.Add(new MailboxAddress(teste, teste2));
            mensagem.To.Add(MailboxAddress.Parse(email));

            mensagem.Subject = assunto;
            var builder = new BodyBuilder { TextBody = mensagemTexto, HtmlBody = mensagemHtml };
            mensagem.Body = builder.ToMessageBody();

            try
            {
                var smtpClient = new SmtpClient();
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await smtpClient.ConnectAsync(EnderecoServicoEmail, PortaServidorEmail, UsaSsl).ConfigureAwait(false);
                await smtpClient.AuthenticateAsync(EmailRemetente, Senha).ConfigureAwait(false);
                await smtpClient.SendAsync(mensagem).ConfigureAwait(false);
                await smtpClient.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
