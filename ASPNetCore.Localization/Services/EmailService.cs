using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace ASPNetCore.Localization.Services
{
    public class EmailService : IEmailService
    {
        public IStringLocalizer<EmailService> Localizer { get; set; }

        public EmailService(IStringLocalizer<EmailService> localizer)
        {
            Localizer = localizer;
        }

        public Task<string> SendEmail(string email, string body, string code)
        {
            var resetEmailContent = Localizer["resetEmail"].Value;


            return  Task.FromResult(resetEmailContent);
        }
    }
}
