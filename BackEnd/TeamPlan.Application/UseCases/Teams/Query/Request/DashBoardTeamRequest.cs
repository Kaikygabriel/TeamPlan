using MediatR;
using TeamPlan.Application.DTOs.StoreFront.Team;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Teams.Query.Request;

public record DashBoardTeamRequest(Guid TeamId,Guid Member)  : IRequest<Result<TeamDashBoardResponse>>;