using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Team : Entity
{
    private Team()
    {
        
    }
    private Team(string name, Member manager)
    {
        Name = name;
        Manager = manager;
        Id = Guid.NewGuid();
    }

    public string Name { get;private set; }
    public Member Manager { get;private set; }
    public Guid ManagerId { get;private set; }
    public List<Member> Members { get; private set; } = new();
    public List<Task> Tasks { get;private set; } = new();
    public List<RecurringTask> RecurringTasks { get;private set; } = new();

    public Enterprise Enterprise { get;private set; }
    public Guid  EnterpriseId { get; set; }
    public ushort PercentageByMonthCurrent { get;private set; }

    public void AddTask(Task task)
        => Tasks.Add(task);
    public void AddRecurringTask(RecurringTask task)
        => RecurringTasks.Add(task);
    public Result FinishTask(Guid taskId)
    {
        var task = Tasks.FirstOrDefault(x => x.Id == taskId);
        if (task is null)
            return Result.Failure(new Error("Task.NotFound", "Task not found"));

        task.Finish();
        UpdatePercentage();

        return Result.Success();
    }

    private void UpdatePercentage()
    {
        var total = Tasks.Count;
        if (total == 0)
        {
            PercentageByMonthCurrent = 0;
            return;
        }

        var done = Tasks.Count(x => !x.Active);
        PercentageByMonthCurrent = (ushort)((done * 100) / total);
    }

    public void AddMember(Member member)
        => Members.Add(member);
    
    public Result RemoveMemberById(Guid id)
    {
        var member = Members.FirstOrDefault(x => x.Id == id);
        if (member is null)
            return new Error("Member.NotFound", "not found");
        Members.Remove(member);
        return Result.Success();
    } 


    public static class Factories
    {
        public static Result<Team> Create(string name, Member manager)
        {
            return Result<Team>.Success(new Team(name,manager));
        }
    }
    
}