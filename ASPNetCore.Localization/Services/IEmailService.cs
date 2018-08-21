using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore.Localization.Services
{
    public interface IEmailService
    {
        Task<String> SendEmail(string email, string body, string code);
    }
}
