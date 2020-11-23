using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace MyDoctor.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
          
            return Task.CompletedTask;
        }
    }
}
