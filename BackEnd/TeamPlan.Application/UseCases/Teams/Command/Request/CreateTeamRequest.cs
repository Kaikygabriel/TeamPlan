using MediatR;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Teams.Command.Request;

public record CreateTeamRequest(string Name, Guid ManageId) : IRequest<Result<Guid>>;