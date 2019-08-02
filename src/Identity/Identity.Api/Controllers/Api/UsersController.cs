using Identity.Api.Models;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

        public UserDto MapToDto(ApplicationUser user)
        {
            return new UserDto()
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                BillingDetails = MapToDto(user.BillingAddress),
                ShippingDetails = MapToDto(user.ShippingAddress)
            };
        }

        public UserDto.AddressDto MapToDto(ApplicationUser.Address address)
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
