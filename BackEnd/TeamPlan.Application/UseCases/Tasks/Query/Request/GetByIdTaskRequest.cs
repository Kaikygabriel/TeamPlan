using MediatR;
using TeamPlan.Application.DTOs.StoreFront.Tasks;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Tasks.Query.Request;

public record GetByIdTaskRequest(Guid TaskId,Guid TeamId) : IRequest<Result<TaskDashBoardDto>>;