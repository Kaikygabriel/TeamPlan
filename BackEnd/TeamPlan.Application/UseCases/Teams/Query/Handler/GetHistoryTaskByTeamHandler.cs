using MediatR;
using TeamPlan.Application.UseCases.Teams.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Query.handler;

public class GetHistoryTaskByTeamHandler : HandlerBase,
    IRequestHandler<GetHistoryTaskByTeamRequest,Result<IEnumerable<Domain.BackOffice.Entities.Task>>>
{
    public GetHistoryTaskByTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<IEnumerable<Domain.BackOffice.Entities.Task>>> Handle(GetHistoryTaskByTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        if (team is null)
            return new Error("Team.NotFound", "Team not found");
        var tasks = await _unitOfWork.TaskRepository.GetTasksByTeamid(request.TeamId);
        return Result<IEnumerable<Domain.BackOffice.Entities.Task>>.Success(tasks);
    }
}