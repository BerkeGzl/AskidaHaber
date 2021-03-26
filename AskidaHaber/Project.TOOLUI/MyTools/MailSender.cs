using System.Net;
using System.Net.Mail;

namespace Project.TOOLUI.MyTools
{
    public static class MailSender
    {
        public static void Send(string receiver, string password = "Berke123", string body = "Deneme", string subject = "Merhaba!", string sender = "burayamailadresgelecek")
        {
            MailAddress senderEmail = new MailAddress(sender);
            MailAddress receiverEmail = new MailAddress(receiver);
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };

            using (MailMessage mesaj = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(mesaj);
            }
        }
    }
}
