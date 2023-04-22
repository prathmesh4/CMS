
using CustMgmSys.Models;

namespace CustMgmSys.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string from, string to, string subject, string body, EmailAttachment attachment = null);
    }

}
