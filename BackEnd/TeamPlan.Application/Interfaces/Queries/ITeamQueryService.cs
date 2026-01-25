using TeamPlan.Application.DTOs.StoreFront.Team;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Application.Interfaces.Queries;

public interface ITeamQueryService
{
    Task<TeamDashBoardResponse> GetTeamDashBoardById(Guid id);
    Task<Team?> GetTeamWithTasksAndKanban(Guid id);
}