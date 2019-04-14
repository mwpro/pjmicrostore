namespace Checkout.Orders.Domain
{
    public class CustomerAddress
    {
        public CustomerAddress(string firstName, string lastName, string address, string city, string zip)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            Zip = zip;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Zip { get; private set; }
    }
}