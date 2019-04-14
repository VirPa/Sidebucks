using Sidebucks.BLL.v1.Methods;
using Sidebucks.BLL.v1.OtherServices.Interface;
using Sidebucks.DAL.v1.Models;
using System.Threading.Tasks;

namespace Sidebucks.BLL.v1.OtherServices {
    public class EmailSender : IEmailSender {

        private readonly EmailDaemon _emailDaemon;

        public EmailSender(EmailDaemon emailDaemon) {

            _emailDaemon = emailDaemon;
        }

        public async Task SendEmailAsync(SendEmailModel model) {

            await _emailDaemon.SendEmailAsync(model);
        }

        public async Task SendEmailWithTemplateAsync(SendEmailWithTemplateModel model) {

            await _emailDaemon.SendEmailWithTemplateAsync(model);
        }
    }
}
