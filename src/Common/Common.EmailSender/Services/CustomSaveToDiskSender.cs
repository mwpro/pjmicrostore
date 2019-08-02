using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core.Models;

namespace Common.EmailSender.Services
{
    public class CustomSaveToDiskSender : ISender
    {
        private readonly string _directory;

        public CustomSaveToDiskSender(string directory)
        {
            _directory = directory;
        }

        public SendResponse Send(IFluentEmail email, CancellationToken? token = null)
        {
            return SendAsync(email, token).GetAwaiter().GetResult();
        }

        public async Task<SendResponse> SendAsync(IFluentEmail email, CancellationToken? token = null)
        {
            var response = new SendResponse();
            await SaveEmailToDisk(email);
            return response;
        }

        private async Task<bool> SaveEmailToDisk(IFluentEmail email)
        {
            var random = new Random();
            var filename = $"{_directory.TrimEnd('\\')}\\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_{random.Next(1000)}_{email.Data.Subject}.html";

            using (var sw = new StreamWriter(File.OpenWrite(filename)))
            {
                sw.WriteLine("<!-- metadata ");
                sw.WriteLine($"From: {email.Data.FromAddress.Name} <{email.Data.FromAddress.EmailAddress}>");
                sw.WriteLine($"To: {string.Join(",", email.Data.ToAddresses.Select(x => $"{x.Name} <{x.EmailAddress}>"))}");
                sw.WriteLine($"Cc: {string.Join(",", email.Data.CcAddresses.Select(x => $"{x.Name} <{x.EmailAddress}>"))}");
                sw.WriteLine($"Bcc: {string.Join(",", email.Data.BccAddresses.Select(x => $"{x.Name} <{x.EmailAddress}>"))}");
                sw.WriteLine($"ReplyTo: {string.Join(",", email.Data.ReplyToAddresses.Select(x => $"{x.Name} <{x.EmailAddress}>"))}");
                sw.WriteLine($"Subject: {email.Data.Subject}");

                sw.WriteLine("-->");
                sw.WriteLine();
                await sw.WriteAsync(email.Data.Body);
            }

            return true;
        }
    }
}