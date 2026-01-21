using MediatR;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Command.Handler;

public class RemoveTeamHandler : HandlerBase,IRequestHandler<RemoveTeamRequest,Result>
{
    public RemoveTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(RemoveTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByIdWithMembersWithTask(request.TeamId);
        if (team is null)
            return new Error("Team.Notfound", "Not Found");
        if(!ManagerIdIsValid(request.Manager,team))
            return new Error("ManagerOrOwner.Notfound", "Not Found");
        
        _unitOfWork.TeamRepository.Delete(team);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }

    private bool ManagerIdIsValid(Guid managerId, Team team)
    {
        if (team.ManagerId == managerId)
            return true;
        if (team.Enterprise?.IdOwner == managerId)
            return true;
        return false;
    }
}