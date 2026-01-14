using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class RecurringTask : Entity
{
    private RecurringTask()
    {
        
    }
    private RecurringTask(string title, string description,Guid teamId,ushort dayMonth,int daysActiveTask)
    {
        TeamId = teamId;
        Title = title;
        Description = description;
        Id = Guid.NewGuid();
        DayMonth = dayMonth;
        DaysActiveTask = daysActiveTask;
    }

    public Team Team { get;private set; }
    public Guid TeamId { get; private set; }
    public ushort DayMonth { get; private set; }
    public int DaysActiveTask { get;private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public Result<Task> CreateTask()
        => Task.Factories.Create(DateTime.Now.AddDays(DaysActiveTask), Title, Description, TeamId);
   
    public static class Factories
    {
        public static Result<RecurringTask> Create
            (string title, string description,Guid teamId,ushort dayMonth,int daysActiveTask)
        {
            return Result<RecurringTask>.Success(new(title, description,teamId,dayMonth,daysActiveTask));
        }
    }
}