using MediatR;
using TeamPlan.Application.DTOs.StoreFront.Team;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Teams.Query.Request;

public record DashBoardTasksInTeamRequest(Guid TeamId)  : IRequest<Result<TaskInTeamDashBoard>>;