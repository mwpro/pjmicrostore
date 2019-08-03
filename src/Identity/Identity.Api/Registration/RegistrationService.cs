using System.Threading.Tasks;
using Identity.Api.Controllers.Account;
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
                // todo require first and last name for registration
                UserName = registerInputModel.Email,
                Email = registerInputModel.Email,
                BillingAddress = new ApplicationUser.Address(null, null, null, null, null), // todo make address nullable
                ShippingAddress = new ApplicationUser.Address(null, null, null, null, null)
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
