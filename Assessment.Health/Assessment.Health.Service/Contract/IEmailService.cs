using Assessment.Health.Domain.Settings;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
