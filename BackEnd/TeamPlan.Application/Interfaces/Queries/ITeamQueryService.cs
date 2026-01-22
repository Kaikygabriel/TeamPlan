using TeamPlan.Application.DTOs.StoreFront.Team;

namespace TeamPlan.Application.Interfaces.Queries;

public interface ITeamQueryService
{
    Task<TeamDashBoardResponse> GetTeamDashBoardById(Guid id);
}