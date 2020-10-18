using System.Threading.Tasks;

namespace LetsDoIt.MailSender
{
    public interface IMailSender
    {
        Task SendAsync(string subject, string htmlContent, params string[] toEmails);

        void Send(string subject, string htmlContent, params string[] toEmails);
    }
}
