using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;
using TeamPlan.Domain.BackOffice.Enum;

namespace TeamPlan.Domain.BackOffice.Entities;

public class RecurringTask : Entity
{
    private RecurringTask()
    {
        
    }
    private RecurringTask(string title, string description,Guid teamId,ushort dayMonth,int daysActiveTask,EPriority priority)
    {
        TeamId = teamId;
        Title = title;
        Description = description;
        Id = Guid.NewGuid();
        DayMonth = dayMonth;
        DaysActiveTask = daysActiveTask;
        Priority = priority;
    }

    public Team Team { get;private set; }
    public Guid TeamId { get; private set; }
    public ushort DayMonth { get; private set; }
    public int DaysActiveTask { get;private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public EPriority Priority { get;private set; }

    public Result<Task> CreateTask()
        => Task.Factories.Create(DateTime.Now.AddDays(DaysActiveTask), Title, Description, TeamId,Priority);
   
    public static class Factories
    {
        public static Result<RecurringTask> Create
            (string title, string description,Guid teamId,ushort dayMonth,int daysActiveTask,EPriority priority)
        {
            return Result<RecurringTask>.Success(new(title, description,teamId,dayMonth,daysActiveTask,priority));
        }
    }
}