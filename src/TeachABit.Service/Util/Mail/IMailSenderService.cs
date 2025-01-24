using System.Net.Mail;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Util.Mail
{
    public interface IMailSenderService
    {
        Task<ServiceResult> SendMail(MailMessage message, string to);
        Task<ServiceResult> SendMail(MailMessage message, string[] to);
    }
}
