using MediatR;
using TeamPlan.Application.DTOs.StoreFront.Team;
using TeamPlan.Application.Interfaces.Queries;
using TeamPlan.Application.UseCases.Teams.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Teams.Query.handler;

public class DashBoardTasksInTeamHandler : IRequestHandler<DashBoardTasksInTeamRequest,Result<TaskInTeamDashBoard>>
{
    private readonly ITeamQueryService _service;

    public DashBoardTasksInTeamHandler(ITeamQueryService service)
    {
        _service = service;
    }

    public async Task<Result<TaskInTeamDashBoard>> Handle(DashBoardTasksInTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _service.GetTeamWithTasksAndKanban(request.TeamId);
        if (team is null)
            return new Error("Team.NotFound", "Not Found");
        var response = new TaskInTeamDashBoard(team, team.Tasks);
        return Result<TaskInTeamDashBoard>.Success(response);
    }
}