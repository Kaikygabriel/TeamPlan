using MediatR;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Enterprises.Command.Handler;

internal class RemoveTeamHandler  : HandlerBase,IRequestHandler<Request.RemoveTeamRequest,Result>
{
    public RemoveTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(Request.RemoveTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository
            .GetByIdWithManagerAndEnterprise(request.TeamId);
        if (team is null || team.Enterprise is null ||team.EnterpriseId != request.EnterpriseId)
            return new Error("Team.NotFound", "Not found!");

        var enterprise = team.Enterprise;
        if (enterprise!.IdOwner != request.OwnerId)
            return new Error("Enterprise.NotFound", "Not Found!");
        
        enterprise.RemoveTeam(team);
        _unitOfWork.TeamRepository.Delete(team);
        await _unitOfWork.CommitAsync();
        
        return Result.Success();
    }
}