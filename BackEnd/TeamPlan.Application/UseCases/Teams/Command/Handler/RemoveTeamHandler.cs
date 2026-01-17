using MediatR;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Command.Handler;

internal class RemoveTeamHandler  : HandlerBase,IRequestHandler<RemoveTeamRequest,Result>
{
    public RemoveTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(RemoveTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByPredicate(x => x.Id == request.TeamId);
        if (team is null || team.EnterpriseId != request.EnterpriseId)
            return Result.Failure(new("Team.NotFound","Team not found!"));
        var enterprise = await _unitOfWork.EnterpriseRepository.GetByPredicate(x => x.Id == request.EnterpriseId);
        if (enterprise is null || enterprise.IdOwner != request.OwnerId)
            return Result.Failure(new("Not.Allowed", "owner not allowed"));
        enterprise.RemoveTeam(team);
        _unitOfWork.TeamRepository.Delete(team);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}
