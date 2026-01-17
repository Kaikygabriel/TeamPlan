using MediatR;
using TeamPlan.Application.DTOs.Team;
using TeamPlan.Application.UseCases.Teams.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Query.handler;

internal class DashBoardTeamHandler : HandlerBase,IRequestHandler<DashBoardTeamRequest,Result<TeamDashBoardDTO>>
{
    public DashBoardTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<TeamDashBoardDTO>> Handle(DashBoardTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByIdWithMembersWithTask(request.TeamId);
        if (team is null)
            return Result<TeamDashBoardDTO>.Failure(new("Team.NotFound", "Team not found!"));
        if(!TeamContainsMember(team,request.Member))
            return Result<TeamDashBoardDTO>.Failure(new("Member.NotFound.InTeam", "Member Not Found In Team"));
        var emailUsers = team.Members.Select(x => x.User.Email.Address);
        var tasksActive = team.Tasks.Where(x => x.Active);
        var marksInProcess = team.Marks.Where(x => !x.Done);
        var response = new TeamDashBoardDTO
            (emailUsers,team.Manager.User.Email.Address,tasksActive,team.PercentageByMonthCurrent,marksInProcess);
        return Result<TeamDashBoardDTO>.Success(response);
    }

    private bool TeamContainsMember(Team team, Guid memberId)
        => team.Members.Exists(x => x.Id == memberId);
}