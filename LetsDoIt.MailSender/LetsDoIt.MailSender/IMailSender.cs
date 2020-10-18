using System.Threading.Tasks;

namespace LetsDoIt.MailSender
{
    public interface IMailSender
    {
        Task Send(string subject, string htmlContent, params string[] toEmails);
    }
}
