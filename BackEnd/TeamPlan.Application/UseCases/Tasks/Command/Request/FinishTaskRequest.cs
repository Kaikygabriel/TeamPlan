using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Tasks.Command.Request;

public record FinishTaskRequest(Guid IdTask,Guid TeamId) : IRequest<Result>;