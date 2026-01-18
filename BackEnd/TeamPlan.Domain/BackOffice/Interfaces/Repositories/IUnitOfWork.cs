using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Enterprise;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Mark;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Member;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Owner;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.RecurringTask;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Task;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Team;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.User;

namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories;

public interface IUnitOfWork
{
    public ITaskRepository TaskRepository { get; set; }
    public ITeamRepository TeamRepository{ get;}
    public IEnterpriseRepository EnterpriseRepository { get;}
    public IUserRepository UserRepository { get;}
    public IMemberRepository MemberRepository { get;}
    public IOwnerRepository OwnerRepository{ get;}
    public IRecurringTaskRepository RecurringTaskRepository { get; }
    public IMarkRepository MarkRepository { get;  }

    System.Threading.Tasks.Task CommitAsync();
}