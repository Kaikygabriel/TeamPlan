using MediatR;
using TeamPlan.Application.DTOs.StoreFront.Team;
using TeamPlan.Application.Interfaces.Queries;
using TeamPlan.Application.UseCases.Teams.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Teams.Query.handler;

internal class DashBoardTeamHandler : IRequestHandler<DashBoardTeamRequest,Result<TeamDashBoardResponse>>
{
    private readonly ITeamQueryService _queryService;

    public DashBoardTeamHandler(ITeamQueryService queryService)
    {
        _queryService = queryService;
    }

    public async Task<Result<TeamDashBoardResponse>> Handle(DashBoardTeamRequest request, CancellationToken cancellationToken)
    {
        var teamDashBoardResponse = await _queryService.GetTeamDashBoardById(request.TeamId);
        
        if (teamDashBoardResponse is null)
            return new Error("Team.NotFound", "Team not found!");
        if(!TeamContainsMember(teamDashBoardResponse,request.Member))
            return new Error("Member.NotFound.InTeam", "Member Not Found In Team");
        
        return Result<TeamDashBoardResponse>.Success(teamDashBoardResponse);
    }

    private bool TeamContainsMember(TeamDashBoardResponse team, Guid memberId)
        => team.IdMembers.Any(x => x == memberId) 
           || memberId == team.ManagerId || team.IdOwner == memberId;
}