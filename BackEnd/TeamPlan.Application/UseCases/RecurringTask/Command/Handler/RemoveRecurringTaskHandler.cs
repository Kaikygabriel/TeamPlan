using MediatR;
using TeamPlan.Application.UseCases.RecurringTask.Command.Request;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.RecurringTask.Command.Handler;

public class RemoveRecurringTaskHandler  : HandlerBase,IRequestHandler<RemoveRecurringTaskRequest,Result>
{
    public RemoveRecurringTaskHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(RemoveRecurringTaskRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        if (team is null)
            return new Error("Team.NotFound", "Not Found");
        
        var recurringTask = await _unitOfWork.RecurringTaskRepository.GetByPredicate
            (x => x.Id == request.RecurringTaskId);
        if (recurringTask is null || recurringTask.TeamId != team.Id)
            return new Error("RecurringTask.NotFound", "Not Found!");
        
        team.RemoveRecurringTaskById(recurringTask);
        _unitOfWork.TeamRepository.Update(team);
        _unitOfWork.RecurringTaskRepository.Delete(recurringTask);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}