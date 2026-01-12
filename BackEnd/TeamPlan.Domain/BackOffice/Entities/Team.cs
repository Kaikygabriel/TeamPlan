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
    
    public List<DoneTask>DoneTasks { get;private set; } = new();

    public Enterprise Enterprise { get;private set; }
    public Guid  EnterpriseId { get; set; }
    public ushort PercentageByMonthCurrent { get;private set; }

    public void AddTask(Task task)
        => Tasks.Add(task);

    public Result FinishTask(Task task,string emailMember)
    {
        if (Tasks.Exists(x => x.Id == task.Id))
            return Result.Failure(new Error("Task.NotFound","Task not found in team"));
        Tasks.Remove(task);
        var taskDone = DoneTask.Factory.Create(task, emailMember);
        if (!taskDone.IsSuccess)
            return taskDone;
        DoneTasks.Add(taskDone.Value);
        UpdatePercentage();
        return Result.Success();
    }

    private void UpdatePercentage()
    {
        var tasksDoneCount = DoneTasks.Count;
        var tasksCount = Tasks.Count;
        if (tasksDoneCount > tasksCount)
            PercentageByMonthCurrent = 100;
        PercentageByMonthCurrent =  (ushort)((ushort)(tasksDoneCount * 100) / tasksCount);
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