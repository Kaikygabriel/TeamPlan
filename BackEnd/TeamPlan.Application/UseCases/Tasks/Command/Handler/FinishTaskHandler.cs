using MediatR;
using TeamPlan.Application.UseCases.Tasks.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Tasks.Command.Handler;

internal class FinishTaskHandler : HandlerBase,IRequestHandler<FinishTaskRequest,Result> 
{
    public FinishTaskHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(FinishTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByPredicate(x => x.Id == request.IdTask);
        if (task is null)
            return Result.Failure(new("Task.NotFound", "task not found!"));
        var team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        var member = await _unitOfWork.MemberRepository.GetByUserIdWithUser(request.UserId);
        var resultDoneTask = team.FinishTask(task,member.User.Email.Address);
        _unitOfWork.TeamRepository.Update(team);
        await _unitOfWork.CommitAsync();
        
        return Result.Success();
    }
}