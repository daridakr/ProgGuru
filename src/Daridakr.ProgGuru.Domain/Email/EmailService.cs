using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Security.Encryption;

namespace Daridakr.ProgGuru.Email
{
    public class EmailService : ITransientDependency
    {
        private readonly IEmailSender _emailSender;

        public StringEncryptionService _encryptionService {get; set;}

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml=true)
        {
            await _emailSender.SendAsync(to, subject, body);

            //var encryptedGmailPassword = _encryptionService.Encrypt("ef821010505");
        }
    }
}
