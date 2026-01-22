using System.Text.Json.Serialization;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Comment : Entity
{
    private Comment()
    {
        
    }
    private Comment(Guid taskId,Task task,Guid memberId, Member member, string message)
    {
        TaskId = taskId;
        Task = task;
        MemberId = memberId;
        Member = member;
        Message = message;
        CreateAt = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }

    public Guid TaskId { get;init;}
    [JsonIgnore]
    public Task Task { get;init; }
    public Guid MemberId { get;init;}
    public Member Member { get;init;}
    public string Message { get;init;}
    public DateTime CreateAt { get;init;}
    [JsonIgnore]
    public Comment? CommentParent { get;private set; }
    public Guid? CommentParentId { get;private  set; }
    
    public List<Comment> SubComments { get; private set; } = new();

    public void AddCommentParent(Comment commentParent)
    {
        CommentParent = commentParent;
        CommentParentId = commentParent.Id;
    }
    
    public static class Factory
    {
        public static Result<Comment> Create(Task task,Member member, string message)
        {
            return Result<Comment>.Success(new(task.Id,task,member.Id,member,message));
        }
    } 
}