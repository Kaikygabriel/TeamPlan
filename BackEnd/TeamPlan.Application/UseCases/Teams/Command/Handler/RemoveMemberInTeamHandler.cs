using MediatR;
using TeamPlan.Application.UseCases.Teams.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Command.Handler;

internal class RemoveMemberInTeamHandler : HandlerBase,IRequestHandler<RemoveMemberInTeamRequest,Result>
{
    public RemoveMemberInTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(RemoveMemberInTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByIdWithMember(request.TeamId);
        if (team is null)
            return new Error("Team.NotFound","not found!");
        if (team.ManagerId != request.ManagerId) 
            return new Error("Manager.NotFound","not found!");
        var resultRemoveMember = team.RemoveMemberById(request.IdMemberRemove);
        if (!resultRemoveMember.IsSuccess)
            return resultRemoveMember.Error;
        return Result.Success();
    }
}