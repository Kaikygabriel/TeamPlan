using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Enum;
using TeamPlan.Domain.BackOffice.ValueObject;
using Task = TeamPlan.Domain.BackOffice.Entities.Task;

namespace TeamPlan.Test.Domain.Backoffice.Entities;

public class TaskTest
{
    
    private readonly Task Task_Valid = 
        Task.Factories.Create(DateTime.Now.AddDays(2),"teste","teste", Guid.NewGuid(), EPriority.High).Value;
    [Theory]
    [InlineData("a","teste",false)]
    [InlineData("teste","a",false)]
    [InlineData("teste","teste",true)]
    public void Should_create_Task_And_Return_Values_Accordingly(string name,string descriptions,bool result)
    {
        var resultCreatedTask = Task.Factories.Create(DateTime.Now.AddDays(1),name, descriptions, Guid.NewGuid(), EPriority.High);
        
        Assert.Equal(result,resultCreatedTask.IsSuccess);
    }

    [Fact]
    public void Should_Return_False_If_Percentage_Task_To_Surpass_100()
    {
        ushort percentageAdded = 200;
        var resultAddedPercentage = Task_Valid.AddPercentage(percentageAdded);
        Assert.False(resultAddedPercentage.IsSuccess);
    }

    [Fact]
    public void Should_Return_False_If_Member_Added_TeamId_Is_Not_Equals_TeamId_Of_Task()
    {
        var member = Member.Factories.Create("teste",
            User.Factories.Create(Password.Factory.Create("testee").Value,Email.Factories.Create("teste@gmail.com").Value).Value).Value;
        var resultAddedTeam = Task_Valid.AddMember(member);
        Assert.False(resultAddedTeam.IsSuccess);
    }
    
    [Fact]
    public void Should_Active_Is_false_when_Finish_Task()
    {
        Task_Valid.Finish();
        Assert.False(Task_Valid.Active);
    }
    
    [Fact]
    public void Should_Return_KanbanCurrent_When_Alter_KanbanPrevius()
    {
        ushort kanbanOrder = 1;
        Task_Valid.AlterCurrentKanban(kanbanOrder);
        Assert.Equal(kanbanOrder,Task_Valid.KanbanCurrent);
    }
    
    [Fact]
    public void Should_True_If_Added_Comment_In_List()
    {
        var member = Member.Factories.Create("teste",
            User.Factories.Create(Password.Factory.Create("testee").Value,Email.Factories.Create("teste@gmail.com").Value).Value).Value;
        var comment = Comment.Factory.Create(Task_Valid, member,"teste").Value;
        Task_Valid.AddComment(comment);
        
        Assert.True(Task_Valid.Comments.Exists(x=>x.Id == comment.Id));
    }
}