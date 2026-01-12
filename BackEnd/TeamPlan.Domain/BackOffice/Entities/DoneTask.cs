using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Domain.BackOffice.Entities;

public class DoneTask
{
    private DoneTask()
    {
        
    } 
    private DoneTask(Task task, string emailMember)
    {
        DateDone= DateTime.Now;
        Task = task;
        EmailMember = emailMember;
    }

    public DateTime DateDone { get;private set; }
    public Task Task { get;private set; }
    public string EmailMember { get;private set; }

    public static class Factory
    {
        public static Result<DoneTask> Create(Task task, string emailMember)
        {
            return Result<DoneTask>.Success(new(task, emailMember));
        }
    }
}