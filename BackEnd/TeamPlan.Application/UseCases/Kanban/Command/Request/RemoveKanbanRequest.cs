using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Kanban.Command.Request;

public record RemoveKanbanRequest(Guid TeamId,string TitleKanban) : IRequest<Result>;