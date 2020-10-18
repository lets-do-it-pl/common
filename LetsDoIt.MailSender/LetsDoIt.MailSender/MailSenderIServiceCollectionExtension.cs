using LetsDoIt.MailSender;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MailSenderIServiceCollectionExtension
    {
        public static IServiceCollection AddMailSender(this IServiceCollection services) => services.AddTransient<IMailSender, MailSender>();
    }
}
