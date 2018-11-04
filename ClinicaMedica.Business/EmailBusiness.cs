using ClinicaMedica.Dto;
using System.Net.Mail;

namespace ClinicaMedica.Business
{
    public class EmailBusiness
    {
        public void Enviar(EmailDto emailDto)
        {
            var mail = new MailMessage();
            var smtp = new SmtpClient();

            mail.From = new MailAddress(emailDto.Remetente, emailDto.Titulo);
            mail.To.Add(emailDto.Destinatario);
            mail.Subject = emailDto.Assunto;
            mail.Body = emailDto.Mensagem;
            mail.IsBodyHtml = true;

            smtp.Send(mail);
        }
    }
}
