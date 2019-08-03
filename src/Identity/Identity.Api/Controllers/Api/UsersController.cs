using Identity.Api.Models;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Identity.Api.Controllers.Account;
using Identity.Api.Registration;
using Identity.Contracts;
using IdentityServer4;

namespace Identity.Api.Controllers.Api
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRegistrationService _registrationService;

        public UsersController(UserManager<ApplicationUser> userManager, IRegistrationService registrationService)
        {
            _userManager = userManager;
            _registrationService = registrationService;
        }


        [HttpGet("{userId}")]
        [Authorize(Scopes.Identities)]// todo not used
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            
            return Ok(MapToDto(user));
        }

        [HttpGet("me")]
        [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByIdAsync(User.GetSubjectId());
            return Ok(MapToDto(user));
        }

        [HttpPost()]
        [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
        public async Task<IActionResult> CreateUser(RegisterInputModel registerInputModel)
        {
            if (!User.HasScope(Scopes.Identities_CreateUser) && false)
            {
                Forbid();
            }

            var (result, applicationUser) = await _registrationService.Register(registerInputModel);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Created($"/api/users/{applicationUser.Id}", MapToDto(applicationUser));
        }

        private UserDto MapToDto(ApplicationUser user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.PhoneNumber,
                BillingDetails = MapToDto(user.BillingAddress),
                ShippingDetails = MapToDto(user.ShippingAddress)
            };
        }

        private UserDto.AddressDto MapToDto(ApplicationUser.Address address)
        {
            if (address == null) return null;

            return new UserDto.AddressDto()
            {
                City = address.City,
                FirstName = address.FirstName,
                LastName = address.LastName,
                Street = address.Street,
                Zip = address.Zip
            };
        }
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public AddressDto ShippingDetails { get; set; }
        public AddressDto BillingDetails { get; set; }

        public class AddressDto
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string Zip { get; set; }
        }
    }
}
