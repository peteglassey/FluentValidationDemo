using FluentValidation;
using Xunit;
using FluentValidation.TestHelper;

namespace FluentValidationDemo;

public class Tests
{
    public Person GetValidPerson() => new()
    {
        FirstName = "John",
        LastName = "Doe",
        DateOfBirth = new DateTime(1990, 1, 1),
        Email = "john.doe@datacom.com",
        PersonalPhoneNumber = "1234567890",
        WorkPhoneNumber = "0987654321",
        Job = new Profession
        {
            JobName = "Software Developer",
            AnnualSalary = 1000000000000
        },
        Addresses =
        [
            new PostalAddress()
            {
                Street = "123 Main St",
                City = "New York",
                State = "NY",
                Country = "USA",
                PostalCode = "10001"
            },
            new Address
            {
                Street = "456 Elm St",
                City = "Los Angeles",
                State = "CA",
                Country = "USA"
            }
        ]
    };
    
    [Fact]
    public void LetsSeeAllTheValidationFailures_DefaultRuleSet()
    {
        var result = new PersonValidator().TestValidate(new Person());

        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public void LetsSeeAllTheValidationFailures_ForAllRuleSet()
    {
        var result = new PersonValidator().TestValidate(new Person(), opt =>
        {
            opt.IncludeAllRuleSets();
        });

        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public void ValidPersonShouldPassValidation()
    {
        var result = new PersonValidator().TestValidate(GetValidPerson());

        result.ShouldNotHaveAnyValidationErrors();
    }
    
        
    [Fact]
    public void PeopleWhoDontHaveEmailAddressShouldHavePhoneNumber_AsAWarning()
    {
        var result = new PersonValidator().TestValidate(new Person());

        result.ShouldHaveValidationErrorFor(m => m.PersonalPhoneNumber)
            .WithSeverity(Severity.Warning);
    }


    [Fact] public void ShouldEnforceDatacomEmailAddress()
    {
        var person = GetValidPerson();
        person.Email = "john@gmail.com";
        
        var result = new PersonValidator().TestValidate(person);

        result.ShouldHaveValidationErrorFor(p => p.Email);
    }
}