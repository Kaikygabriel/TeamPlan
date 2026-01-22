using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Test.Domain.Backoffice.ValueObjects;

public class EmailTest
{
    private const string Address_Valid = "teste@gmail.com";
    private const string Address_Not_Constraint_Arroba = "teste";
    private const string Address_Small = "tes";


    [Fact]
    public void Should_Return_False_If_Address_In_Email_Is_Small()
    {
        var resultCreateEmail = Email.Factories.Create(Address_Small);
        Assert.False(resultCreateEmail.IsSuccess);
    }
    [Fact]
    public void Should_Return_False_If_Address_Not_Constraint_Arroba()
    {
        var resultCreateEmail = Email.Factories.Create(Address_Not_Constraint_Arroba);
        Assert.False(resultCreateEmail.IsSuccess);
    }
    [Fact]
    public void Should_Return_True_If_Address_Is_Valid()
    {
        var resultCreateEmail = Email.Factories.Create(Address_Valid);
        Assert.True(resultCreateEmail.IsSuccess);
    }
}