using System.Net;
using System.Net.Mail;
using static Pecanha.Service.Helpers.ConfigHelper;

namespace Pecanha.Service.Handlers {
    public class EmailHandler {
        public static bool SendEmail(string remetente, string assunto, string corpoEmail) {
            bool ret = true;
            try {
                Config config = GetEmailSendConfiguration();
                SmtpClient smtpClient = null;
                smtpClient = BuildSmtpClient(config);

                MailMessage message = BuildEmailMessage(assunto, config, corpoEmail, remetente);
                message.To.Add(config.AddressTo);
                smtpClient.Send(message);
            } catch {
                // Não me preocupei com um controle muito sofisticado para envio de email, para por exemplo, retonar os motivos do não envio e etc.
                ret = false;
            }
            return ret;
        }

        private static SmtpClient BuildSmtpClient(Config config) {
            return new SmtpClient {
                Host = config.Host,
                Port = config.Port,
                EnableSsl = config.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(config.FromAddress, config.Password)
            };
        }

        private static MailMessage BuildEmailMessage(string assunto, Config config, string corpoEmail, string remetente) {
            return new MailMessage() {
                From = new MailAddress(config.FromAddress, remetente),
                Subject = assunto,
                Body = corpoEmail,
                IsBodyHtml = false
            };
        }
    }
}
