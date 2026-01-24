using MediatR;
using TeamPlan.Application.UseCases.Kanban.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Kanban.Command.Handler;

public class AddKanbanHandler : HandlerBase,IRequestHandler<AddKanbanRequest,Result>
{
    public AddKanbanHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(AddKanbanRequest request, CancellationToken cancellationToken)
    {
        var resultCreateKanban = request.ToEntity();
        if (!resultCreateKanban.IsSuccess)
            return resultCreateKanban.Error;
        var kanban = resultCreateKanban.Value;

        var team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        if (team is null)
            return new Error("Team.NotFound", "Not Found!");
        team.AddKanban(kanban);
        _unitOfWork.TeamRepository.Update(team);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}