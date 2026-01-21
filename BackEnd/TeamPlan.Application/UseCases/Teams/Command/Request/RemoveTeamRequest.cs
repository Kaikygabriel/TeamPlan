using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Teams.Command.Request;

public record RemoveTeamRequest(Guid TeamId,Guid Manager) : IRequest<Result>;