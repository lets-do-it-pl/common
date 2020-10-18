namespace LetsDoIt.MailSender.Options
{
    public class SmtpOptions
    {
        /// <summary>
        /// Smtp Section Name is "Smtp"
        /// </summary>
        public const string SmtpSectionName = "Smtp";

        public string Host { get; set; }

        public int Port { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
