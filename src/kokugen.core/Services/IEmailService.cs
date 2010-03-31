#region Using Directives

using System.Net;
using System.Net.Mail;
using Kokugen.Core.Domain;

#endregion

namespace Kokugen.Core.Services
{
    public interface IEmailService
    {
        void SendPasswordResetEmail(User user);
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(SmtpClient smtpClient, string mailUser, string mailPassword, bool authRequired)
        {
            _smtpClient = smtpClient;
            if (authRequired)
                _smtpClient.Credentials = new NetworkCredential(mailUser, mailPassword);
        }

        #region IEmailService Members

        public void SendPasswordResetEmail(User user)
        {
            const string message = "";
            SendEmail(user.EmailAddress,"test@test.com","Password Reset", message);
        }

        private void SendEmail(string to, string from, string subject, string body)
        {
            var msg = new MailMessage(from, to, subject, body);
            _smtpClient.Send(msg);
        }

        #endregion
    }
}