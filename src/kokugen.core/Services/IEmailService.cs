#region Using Directives

using System.Net;
using System.Net.Mail;
using System.Web.Security;
using Kokugen.Core.Membership.Security;

#endregion

namespace Kokugen.Core.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string from, string subject, string body);
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

        public void SendEmail(string to, string from, string subject, string body)
        {
            var msg = new MailMessage(from, to, subject, body){IsBodyHtml = true};
            _smtpClient.Send(msg);
        }

        #endregion
    }
}