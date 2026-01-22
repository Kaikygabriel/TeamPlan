using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Domain.BackOffice.ValueObject;

public class Email
{
    private Email()
    {
        
    }
    private Email(string address)
    {
        Address = address;
    }

    public string Address { get;private set; }


    public static class Factories
    {
        public static Result<Email> Create(string address)
        {
            if (AddressIsInvalid(address))
                return Result<Email>.Failure(new("Address.Invalid","Address in email invalid!"));
            return Result<Email>.Success(new(address));
        }
    }

    private static bool AddressIsInvalid(string address)
        => string.IsNullOrWhiteSpace(address) || address.Length <= 3 || !address.Contains('@');
}