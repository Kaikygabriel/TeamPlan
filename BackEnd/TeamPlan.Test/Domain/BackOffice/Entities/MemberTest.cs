using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Test.Domain.Backoffice.Entities;

public class MemberTest
{
    private readonly User User_Valid = User.Factories.Create
        (Password.Factory.Create("teste@").Value,Email.Factories.Create("teste@gmail.com").Value).Value;
    
    [Fact]
    public void Should_Return_false_If_NameInMember_Is_Invalid()
    {
        //arrange
        var name = "ab";
        
        //act
        var result = Member.Factories.Create(name,User_Valid);
        
        //assert
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Return_True_if_Parameters_Are_Valid()
    {
        //arrange
        var name = "teste";
        
        //act
        var result = Member.Factories.Create(name,User_Valid);
        
        //assert
        Assert.True(result.IsSuccess);
    }
}