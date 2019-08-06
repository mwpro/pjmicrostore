using System.Threading.Tasks;
using Identity.Api.Models;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Api.Registration
{
    public interface IRegistrationService
    {
        Task<(IdentityResult result, ApplicationUser user)> Register(RegisterInputModel registerInputModel);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegistrationService> _logger;

        public RegistrationService(UserManager<ApplicationUser> userManager, ILogger<RegistrationService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<(IdentityResult result, ApplicationUser user)> Register(RegisterInputModel registerInputModel)
        {
            var user = new ApplicationUser
            {
                UserName = registerInputModel.Email,
                Email = registerInputModel.Email,
                BillingAddress = ApplicationUser.Address.Empty(), // todo make address nullable
                ShippingAddress = ApplicationUser.Address.Empty()
            };
            var result = await _userManager.CreateAsync(user, registerInputModel.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
            }
            return (result, user);
        }
    }
}
