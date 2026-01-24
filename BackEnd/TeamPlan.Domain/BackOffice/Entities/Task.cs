using System.Text.Json.Serialization;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;
using TeamPlan.Domain.BackOffice.Enum;
using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Task: Entity
{
    private Task()
    {
        
    }
    private Task(DateTime endDate, string title, string description,Guid teamId,EPriority priority)
    {
        TeamId = teamId;
        EndDate = endDate;
        Title = title;
        Description = description;
        CreateAt = DateTime.Now;
        Id = Guid.NewGuid();
        Active = true;
        Priority = priority;
    }

    public Guid? MemberId { get;private set; }
    public Member? Member { get;private set; }
    public DateTime CreateAt { get; init; }
    public DateTime EndDate { get;init; }
    public ushort Percentage { get;private set; }
    public string Title { get;init; }
    public  string Description { get;init; }
    public bool Active { get;private set; }
    public Guid TeamId { get;private set; }
    [JsonIgnore]
    public Team Team { get;private set; }
    public EPriority Priority { get;init; }
    public List<Comment>Comments { get;private set; } = new();
    public ushort? KanbanCurrent {get; private set; }
    
    public void AlterCurrentKanban(ushort order)
        => KanbanCurrent = order;
    
    public void AddComment(Comment comment)
        => Comments.Add(comment); 
    
    public void Finish()
        => Active = false;
    
    public Result AddMember(Member member)
    {
        if (member.TeamId != TeamId)
            return Result.Failure(new("Member.IdTeam.Invalid", "IdTeam member is not equals idTeam task"));
        Member = member;
        return Result.Success();
    }

    
    public Result AddPercentage(ushort value)
    {
        if (Percentage > 100)
            return new Error("Percentage.Invalid", "Percentage > 100");
        var sum = Math.Min(100, Percentage + value);
        
        if (Percentage == 100)
            Finish();
        else
            Percentage = (ushort)sum;
        
        return Result.Success();
    }

   
    public static class Factories
    {
        public static Result<Task> Create
            (DateTime endDate, string title, string description,Guid teamId,EPriority priority)
        {
            return Result<Task>.Success(new(endDate, title, description,teamId,priority));
        }
    }
    
}