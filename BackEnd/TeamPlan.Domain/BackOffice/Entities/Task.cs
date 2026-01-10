using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Task: Entity
{
    private Task()
    {
        
    }
    public Task(Member member, DateTime endDate, ushort percentage, string title, string description)
    {
        Member = member;
        EndDate = endDate;
        Percentage = percentage;
        Title = title;
        Description = description;
        CreateAt = DateTime.Now;
        Id = Guid.NewGuid();
    }

    public Member Member { get;private set; }
    public DateTime CreateAt { get;private set; }
    public DateTime EndDate { get;private set; }
    public ushort Percentage { get;private set; }
    public string Title { get;private set; }
    public  string Description { get;private set; }
    
    public static class Factories
    {
        public static Result<Task> Create
            (Member member, DateTime endDate, ushort percentage, string title, string description)
        {
            return Result<Task>.Success(new(member, endDate, percentage, title, description));
        }
    }
}