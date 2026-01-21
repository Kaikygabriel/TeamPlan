using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Comment;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Enterprise;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Mark;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Member;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Owner;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.RecurringTask;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Task;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Team;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.User;
using TeamPlan.Infra.Data.Context;
using TeamPlan.Infra.Repositories.Comment;
using TeamPlan.Infra.Repositories.Enterprise;
using TeamPlan.Infra.Repositories.Marks;
using TeamPlan.Infra.Repositories.Member;
using TeamPlan.Infra.Repositories.Owner;
using TeamPlan.Infra.Repositories.RecurringTask;
using TeamPlan.Infra.Repositories.Task;
using TeamPlan.Infra.Repositories.Team;
using TeamPlan.Infra.Repositories.User;

namespace TeamPlan.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private MarkRepository _markRepository;
    private RecurringTaskRepository _recurringTaskRepository;
    private OwnerRepository _ownerRepository;
    private MemberRepository _memberRepository;
    private UserRepository _userRepository;
    private TeamRepository _teamRepository;
    private TaskRepository _taskRepository;
    private EnterpriseRepository _enterpriseRepository;
    private CommentRepository _commentRepository;
    
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ITaskRepository TaskRepository
    {
        get
        {
            return _taskRepository = _taskRepository ?? new(_context);
        }
        set;
    }

    public ITeamRepository TeamRepository
    {
        get
        {
            return _teamRepository = _teamRepository ?? new(_context);
        }
    }

    public IEnterpriseRepository EnterpriseRepository
    {
        get
        {
            return _enterpriseRepository = _enterpriseRepository ?? new(_context);
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _userRepository = _userRepository ?? new(_context);
        }
    }

    public IMemberRepository MemberRepository
    {
        get
        {
            return _memberRepository = _memberRepository ?? new(_context);
        }
    }

    public IOwnerRepository OwnerRepository
    {
        get
        {
            return _ownerRepository = _ownerRepository ?? new (_context);
        }
    }

    public IRecurringTaskRepository RecurringTaskRepository
    {
        get
        {
            return _recurringTaskRepository = _recurringTaskRepository ?? new(_context);
        }
    }

    public IMarkRepository MarkRepository
    {
        get
        {
            return _markRepository = _markRepository ?? new(_context);

        }
    }

    public ICommentRepository CommentRepository
    {
        get
        {
            return _commentRepository = _commentRepository ?? new(_context);
        }
    }

    public async System.Threading.Tasks.Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}