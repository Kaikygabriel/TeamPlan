using MediatR;
using TeamPlan.Application.UseCases.Kanban.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Kanban.Command.Handler;

public class RemoveKanbanHandler : HandlerBase,IRequestHandler<RemoveKanbanRequest,Result>
{
    public RemoveKanbanHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(RemoveKanbanRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        if (team is null)
            return new Error("Task.NotFound","not found");
        var resultRemoveKanban = team.RemoveKanbanByName(request.TitleKanban);
        if (!resultRemoveKanban.IsSuccess)
            return resultRemoveKanban.Error;
        
        _unitOfWork.TeamRepository.Update(team);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}