using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Common.EmailSender.Infrastructure
{
    public static class EmailServiceCollectionExtensionsExtensions
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection serviceCollection)
        {
            Email.DefaultSender = new CustomSaveToDiskSender(EmailConstants.SentMails);
            Email.DefaultRenderer = new CustomRazorRenderer(EmailConstants.TemplateDir);

            return serviceCollection;
        }
    }
}