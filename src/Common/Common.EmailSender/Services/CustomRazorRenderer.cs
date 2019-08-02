using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core.Interfaces;
using RazorLight;
using RazorLight.Razor;

namespace Common.EmailSender.Services
{
    public class CustomRazorRenderer : ITemplateRenderer
    {
        private readonly RazorLightEngine _engine;
        
        public CustomRazorRenderer(string root)
        {
            _engine = new RazorLightEngineBuilder()
                .UseFilesystemProject(root ?? Directory.GetCurrentDirectory())
                .UseMemoryCachingProvider()
                .Build();
        }
        
        public async Task<string> ParseAsync<T>(string template, T model, bool isHtml = true)
        {
            //dynamic viewBag = (model as IViewBagModel)?.ViewBag;
            return await _engine.CompileRenderAsync<T>(GetHashString(template), template, model);
        }

        string ITemplateRenderer.Parse<T>(string template, T model, bool isHtml)
        {
            return ParseAsync(template, model, isHtml).GetAwaiter().GetResult();
        }

        public static string GetHashString(string inputString)
        {
            var sb = new StringBuilder();
            var hashbytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
            foreach (byte b in hashbytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}