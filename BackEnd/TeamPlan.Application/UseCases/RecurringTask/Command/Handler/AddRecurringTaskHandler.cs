using MediatR;
using TeamPlan.Application.UseCases.RecurringTask.Command.Request;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.RecurringTask.Command.Handler;

internal class AddRecurringTaskHandler : HandlerBase, IRequestHandler<AddRecurringTaskRequest,Result>
{
    public AddRecurringTaskHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(AddRecurringTaskRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        if (team is null)
            return new Error("Team.NotFound", "Not Found!");
        var recurringTaskResultCreate = request.ToEntity();
        if (!recurringTaskResultCreate.IsSuccess)
            return recurringTaskResultCreate.Error;
        var recurringTask = recurringTaskResultCreate.Value;
        
        team.AddRecurringTask(recurringTask);
        _unitOfWork.TeamRepository.Update(team);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}