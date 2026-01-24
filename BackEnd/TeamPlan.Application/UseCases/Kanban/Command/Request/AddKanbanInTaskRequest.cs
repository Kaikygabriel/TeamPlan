using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
 
namespace TeamPlan.Application.UseCases.Kanban.Command.Request;

public record AddKanbanInTaskRequest(Guid TaskId,Guid TeamId,string TitleKanban) : IRequest<Result>;