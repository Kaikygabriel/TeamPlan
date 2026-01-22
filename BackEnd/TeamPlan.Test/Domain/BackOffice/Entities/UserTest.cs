using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Test.Domain.Backoffice.Entities;

public class UserTest
{

    [Fact]
    public void Should_Return_True_When_Email_And_Password_Are_Valid()
    {
        //Arrange
        var email = Email.Factories.Create("teste@gmail.com").Value;
        var password = Password.Factory.Create("teste@123").Value;
        //act
        var user = User.Factories.Create(password, email);
        //Assert
        Assert.True(user.IsSuccess
        );
    }
    
}