namespace FluentValidationDemo;

public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public string Email { get; set; } = string.Empty;
    public string PersonalPhoneNumber { get; set; } = string.Empty;
    public string WorkPhoneNumber { get; set; } = string.Empty;
    public Profession Job { get; set; } = new ();
    public List<IAddress> Addresses { get; set; } = new();
}

public class Profession
{
    public string JobName { get; set; } = string.Empty;
    public decimal AnnualSalary { get; set; } = 0.0M;
}

public class PostalAddress : IAddress
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}

public class Address : IAddress
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public interface IAddress
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}