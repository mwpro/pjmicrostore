using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Address ShippingAddress { get; set;  }
        public Address BillingAddress { get; set; }

        public class Address
        {
            public static Address Empty() => new Address(null, null, null, null, null);

            public Address(string firstName, string lastName, string street, string city, string zip)
            {
                FirstName = firstName;
                LastName = lastName;
                Street = street;
                City = city;
                Zip = zip;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string Zip { get; set; }
        }
    }
}
