using MediatR;
using TeamPlan.Application.DTOs.Tasks;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Tasks.Query.Request;

public record GetByIdTaskRequest(Guid TaskId,Guid TeamId) : IRequest<Result<GetTaskDto>>;