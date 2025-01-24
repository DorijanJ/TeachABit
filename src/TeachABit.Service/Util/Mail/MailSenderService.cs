using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.Service.Util.Mail
{
    public class MailSenderService(IConfiguration configuration) : IMailSenderService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<ServiceResult> SendMail(MailMessage message, string to)
        {
            string? apiKey = _configuration["SMTP:ApiKey"];
            string? server = _configuration["SMTP:Server"];
            string? from = _configuration["SMTP:SenderEmail"];
            string? fromName = _configuration["SMTP:SenderName"];
            int? port = int.TryParse(_configuration["SMTP:Port"], out int parsedPort) ? parsedPort : null;

            if (string.IsNullOrEmpty(apiKey) ||
                string.IsNullOrEmpty(server) ||
                string.IsNullOrEmpty(from) ||
                string.IsNullOrEmpty(fromName) ||
                !port.HasValue)
            {
                return ServiceResult.Failure(MessageDescriber.MissingConfiguration());
            }

            using var client = new SmtpClient(server, port.Value)
            {
                Credentials = new NetworkCredential("apikey", apiKey),
                EnableSsl = true,
            };

            try
            {
                message.From = new MailAddress(from, fromName);
                message.To.Add(to);
                await client.SendMailAsync(message);
                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure(new MessageResponse($"{ex.Message}", MessageSeverities.Error));
            }
        }

        public async Task<ServiceResult> SendMail(MailMessage message, string[] to)
        {
            string? apiKey = _configuration["SMTP:ApiKey"];
            string? server = _configuration["SMTP:Server"];
            string? from = _configuration["SMTP:SenderEmail"];
            string? fromName = _configuration["SMTP:SenderName"];
            int? port = int.TryParse(_configuration["SMTP:Port"], out int parsedPort) ? parsedPort : null;

            if (string.IsNullOrEmpty(apiKey) ||
                string.IsNullOrEmpty(server) ||
                string.IsNullOrEmpty(from) ||
                string.IsNullOrEmpty(fromName) ||
                !port.HasValue)
            {
                return ServiceResult.Failure(MessageDescriber.MissingConfiguration());
            }

            using var client = new SmtpClient(server, port.Value)
            {
                Credentials = new NetworkCredential("apikey", apiKey),
                EnableSsl = true,
            };

            try
            {
                message.From = new MailAddress(from, fromName);

                foreach (var recipient in to)
                {
                    if (!string.IsNullOrWhiteSpace(recipient))
                    {
                        message.To.Add(new MailAddress(recipient));
                    }
                }

                if (message.To.Count == 0)
                {
                    return ServiceResult.Failure(MessageDescriber.BadRequest("Barem jedan mail mora biti u listi."));
                }

                await client.SendMailAsync(message);
                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure(new MessageResponse(ex.Message, MessageSeverities.Error));
            }
        }

    }
}
