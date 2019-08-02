using System;
using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration;

namespace Common.EmailSender.Infrastructure
{
    public interface ISendMailService
    {
        Task SendMail<TModel>(string recipient, string subject, string templateName, TModel model);
    }

    public class SendMailService : ISendMailService
    {
        private readonly IConfiguration _configuration;

        public SendMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMail<TModel>(string recipient, string subject, string templateName, TModel model)
        {
            var email = await Email
                .From(_configuration.GetValue<string>("Emails:DefaultFrom:Email"),
                    _configuration.GetValue<string>("Emails:DefaultFrom:Name"))
                .To(recipient)
                .Subject(subject)
                .UsingTemplateFromFile($"{EmailConstants.TemplateDir}/{templateName}.cshtml", model)
                .SendAsync();

            if (!email.Successful)
                throw new Exception($"Sending mail failed because: ${string.Join(", ", email.ErrorMessages)}");
        }
    }
}