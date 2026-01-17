using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Mark : Entity
{
    private Mark()
    {
        
    }
    private Mark(string title, string description, ushort taskCount,Guid teamId)
    {
        Id = Guid.NewGuid();
        Title = title;
        Descriptor = description;
        TaskCount = taskCount;
        TeamId = teamId;
    }

    public Team Team { get;private set; }
    public Guid TeamId { get;private set; }
    public string Title { get;private set; }
    public string Descriptor { get;private set; }
    public ushort TaskCount { get;private set; }
    public ushort TaskCountDone { get;private set; }
    public ushort Percentage { get;private set; }
    public bool Done { get;private set; }
    
    public void OneTaskCompleted()
    {
        if (TaskCountDone > TaskCount)
        {
            Done = true;
            return;
        }
        TaskCountDone++;
        UpdatePercentage();
    }

    public void UpdatePercentage()
        => Percentage = (ushort)(((ushort)TaskCountDone * 100) / TaskCount);

    public class Factory
    {
        public static Result<Mark> Create(string title, string description, ushort taskCount,Guid teamId)
            => Result<Mark>.Success(new(title, description, taskCount,teamId));
    }
}