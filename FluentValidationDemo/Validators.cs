using FluentValidation;

namespace FluentValidationDemo;


public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty()
            .DependentRules(() => // only check first name if last name is not null
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty();
            });
        
        RuleFor(person => person.Email)
            .EmailAddress();
        
        RuleFor(person => person.Email)
            .Must(e => e.EndsWith("datacom.com"))
            .WithMessage("'{PropertyName}' Must be a Datacom email address");

        RuleFor(person => person.PersonalPhoneNumber)
            .SetValidator(new PhoneNumberValidator())
            .WithMessage("{PropertyName} must be valid phone number");

        RuleFor(person => person.WorkPhoneNumber)
            .SetValidator(new PhoneNumberValidator())
            .WithMessage("{PropertyName} must be valid phone number");
        
        RuleFor(person => person.DateOfBirth)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now);

        When(person => string.IsNullOrEmpty(person.Email), () =>
        {
            RuleFor(person => person.PersonalPhoneNumber)
                .NotEmpty()
                .WithSeverity(Severity.Warning)
                .WithMessage("Should have a phone number when no email address provided");
        });

        RuleSet(nameof(Person.Addresses), () =>
        {
            RuleFor(person => person.Addresses)
                .Must(addresses => addresses.Count() >= 1)
                .WithMessage("At least one address must be provided");
            
            RuleForEach(x => x.Addresses)
                .SetInheritanceValidator(v =>
                {
                    v.Add(new PostalAddressValidator());
                    v.Add(new AddressValidator());
                });
        });
        
        RuleFor(m => m.Job)
            .ChildRules(job =>
            {
                job.RuleFor(j => j.JobName)
                    .NotEmpty()
                    .WithMessage("{PropertyName} must have a valid job title");

                job.RuleFor(j => j.AnnualSalary)
                    .Custom((salary, context) =>
                    {
                        if(salary <= 0m)
                            context.AddFailure("We dont work for free, bro!");
                    });
            });
    }
}

public class PhoneNumberValidator : AbstractValidator<string>
{
    public PhoneNumberValidator()
    {
        RuleFor(m => m)
            .Matches(@"^\d{10}$")
            .WithName("PhoneNumber");
    }
}

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(address => address.Street)
            .NotEmpty();

        RuleFor(address => address.City)
            .NotEmpty();

        RuleFor(address => address.State)
            .NotEmpty();

        RuleFor(address => address.Country)
            .NotEmpty();
    }
}

public class PostalAddressValidator : AbstractValidator<PostalAddress>
{
    public PostalAddressValidator()
    {
        RuleFor(address => address.Street)
            .NotEmpty();

        RuleFor(address => address.City)
            .NotEmpty();

        RuleFor(address => address.State)
            .NotEmpty();

        RuleFor(address => address.Country)
            .NotEmpty();

        RuleFor(address => address.PostalCode)
            .MaximumLength(4)
            .NotEmpty();
    }
}
