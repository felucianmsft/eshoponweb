using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

public class ServiceRefactor
{
    public Customer UpsertCustomer(Customer customer)
    {
        //Null and whitespace checks
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        if (customer.Id <= 0)
        {
            throw new ArgumentException("Invalid customer ID.", nameof(customer));
        }
        if (string.IsNullOrWhiteSpace(customer.FirstName))
        {
            throw new ArgumentException("Invalid customer first name.", nameof(customer));
        }
        if (string.IsNullOrWhiteSpace(customer.LastName))
        {
            throw new ArgumentException("Invalid customer last name.", nameof(customer));
        }

        if (string.IsNullOrWhiteSpace(customer.Address))
        {
            throw new ArgumentException("Invalid customer address.", nameof(customer));
        }

        if (string.IsNullOrWhiteSpace(customer.City))
        {
            throw new ArgumentException("Invalid customer city.", nameof(customer));
        }

        if (string.IsNullOrWhiteSpace(customer.State))
        {
            throw new ArgumentException("Invalid customer state.", nameof(customer));
        }

        //Phone number validation
        if (string.IsNullOrWhiteSpace(customer.Phone))
        {
            throw new ArgumentException("Invalid customer phone number.", nameof(customer));
        }

        //Zip code validation
        bool isValidZipCode = false;
        switch (customer.State.ToUpperInvariant())
        {
            case "US":
                // US zip code validation
                Regex regex = new Regex(@"^\d{5}(?:[-\s]\d{4})?$", RegexOptions.IgnoreCase);
                isValidZipCode = regex.IsMatch(customer.ZipCode);
                break;
            case "ITALY":
                // Italy zip code validation
                regex = new Regex(@"^\d{5}$");
                isValidZipCode = regex.IsMatch(customer.ZipCode);
                break;
            case "GERMANY":
                // Germany zip code validation
                regex = new Regex(@"^\d{5}$");
                isValidZipCode = regex.IsMatch(customer.ZipCode);
                break;
            case "FRANCE":
                // France zip code validation
                regex = new Regex(@"^\d{5}$");
                isValidZipCode = regex.IsMatch(customer.ZipCode);
                break;
            default:
                throw new ArgumentException("Invalid customer country.", nameof(customer));
        }

        //Email validation
        try
        {
            var addr = new System.Net.Mail.MailAddress(customer.Email);

            // Verify that the email address has a valid domain name
            var host = addr.Host;
            var mxRecords = Dns.GetHostAddresses(host).Where(x => x.AddressFamily == AddressFamily.InterNetwork);
            if (!mxRecords.Any())
            {
                throw new ArgumentException("Invalid customer email address.", nameof(customer));
            }

            // Verify that the email address has a valid username
            var username = addr.User;
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Invalid customer email address.", nameof(customer));
            }

            // Verify that the email address has a valid top-level domain
            var tld = addr.Host.Split('.').LastOrDefault();
            if (string.IsNullOrWhiteSpace(tld) || tld.Length < 2 || tld.Length > 6)
            {
                throw new ArgumentException("Invalid customer email address.", nameof(customer));
            }

        }
        catch
        {
            throw new ArgumentException("Invalid customer email address.", nameof(customer));
        }

        //Age validation
        if (customer.DateOfBirth > DateTime.Now || customer.DateOfBirth < DateTime.Now.AddYears(-100))
        {
            throw new ArgumentException("Invalid customer date of birth.", nameof(customer));
        }

        DataRepository.UpsertCustomer(customer);

        return customer;
    }

}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Phone: {Phone}, Address: {Address}, City: {City}, State: {State}, ZipCode: {ZipCode}, DateOfBirth: {DateOfBirth}";
    }
}




























