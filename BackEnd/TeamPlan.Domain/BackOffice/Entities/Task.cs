using System.Text.Json.Serialization;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Task: Entity
{
    private Task()
    {
        
    }
    private Task(DateTime endDate, string title, string description,Guid teamId)
    {
        TeamId = teamId;
        EndDate = endDate;
        Title = title;
        Description = description;
        CreateAt = DateTime.Now;
        Id = Guid.NewGuid();
        Active = true;
    }

    public Guid? MemberId { get; set; }
    public Member? Member { get;private set; }
    public DateTime CreateAt { get;private set; }
    public DateTime EndDate { get;private set; }
    public ushort Percentage { get;private set; }
    public string Title { get;private set; }
    public  string Description { get;private set; }
    public bool Active { get;private set; }
    public Guid TeamId { get;private set; }
    [JsonIgnore]
    public Team Team { get;private set; }

    public Result AddMember(Member member)
    {
        if (member.TeamId != TeamId)
            return Result.Failure(new("Member.IdTeam.Invalid", "IdTeam member is not equals idTeam task"));
        Member = member;
        return Result.Success();
    }

    public void Finish()
        => Active = false;
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
            (DateTime endDate, string title, string description,Guid teamId)
        {
            return Result<Task>.Success(new(endDate, title, description,teamId));
        }
    }
    
}