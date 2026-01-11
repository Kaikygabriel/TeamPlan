using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Task: Entity
{
    private Task()
    {
        
    }
    public Task(DateTime endDate, string title, string description,Guid teamId)
    {
        TeamId = teamId;
        EndDate = endDate;
        Title = title;
        Description = description;
        CreateAt = DateTime.Now;
        Id = Guid.NewGuid();
    }

    public Member? Member { get;private set; }
    public DateTime CreateAt { get;private set; }
    public DateTime EndDate { get;private set; }
    public ushort Percentage { get;private set; }
    public string Title { get;private set; }
    public  string Description { get;private set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; }

    public Result AddMember(Member member)
    {
        if (member.TeamId != TeamId)
            return Result.Failure(new("Member.IdTeam.Invalid", "IdTeam member is not equals idTeam task"));
        Member = member;
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