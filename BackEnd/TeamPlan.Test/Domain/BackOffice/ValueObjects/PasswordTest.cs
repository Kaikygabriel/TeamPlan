using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Test.Domain.Backoffice.ValueObjects;

public class PasswordTest
{
    private const string Password_Valid = "teste@123";
    private const string Password_Small = "te";
    private const string Password_Empty = "";

    [Fact]
    public void Should_Return_False_If_Password_Is_Small()
    {
        var resultCreatePassword = Password.Factory.Create(Password_Small);
        Assert.False(resultCreatePassword.IsSuccess);
    }
    [Fact]
    public void Should_Return_False_If_Password_Is_Emtpy()
    {
        var resultCreatePassword = Password.Factory.Create(Password_Empty);
        Assert.False(resultCreatePassword.IsSuccess);
    }
    [Fact]
    public void Should_Return_True_If_Password_Is_Valid()
    {
        var resultCreatePassword = Password.Factory.Create(Password_Valid);
        Assert.True(resultCreatePassword.IsSuccess);
    }
}