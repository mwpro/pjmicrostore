using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.EmailSender.Infrastructure
{
    public static class EmailServiceCollectionExtensionsExtensions
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            Email.DefaultSender = new SmtpSender(() => new SmtpClient(
                configuration.GetValue<string>("Emails:Smtp:Host"), 
                configuration.GetValue<int>("Emails:Smtp:Port")));
            Email.DefaultRenderer = new CustomRazorRenderer(EmailConstants.TemplateDir);

            return serviceCollection;
        }
    }
}