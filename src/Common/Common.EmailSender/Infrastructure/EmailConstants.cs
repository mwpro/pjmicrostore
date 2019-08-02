using System.IO;

namespace Common.EmailSender.Infrastructure
{
    public static class EmailConstants
    {
        public static string TemplateDir => $"{Directory.GetCurrentDirectory()}/Templates/";
        public static string SentMails => $"{Directory.GetCurrentDirectory()}/Sent/";
    }
}