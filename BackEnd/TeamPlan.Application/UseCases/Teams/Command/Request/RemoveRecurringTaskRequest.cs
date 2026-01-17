using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Teams.Command.Request;

public record RemoveRecurringTaskRequest(Guid RecurringTaskId, Guid TeamId) : IRequest<Result>;