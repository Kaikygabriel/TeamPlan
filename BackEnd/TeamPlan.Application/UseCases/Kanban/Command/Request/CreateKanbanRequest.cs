using MediatR;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Kanban.Command.Request;

public record CreateKanbanRequest(string NameKanban, ushort Order, Guid TeamId) : IRequest<Result>
{
    public Result<Domain.BackOffice.ValueObject.Kanban> ToEntity()
        => Domain.BackOffice.ValueObject.Kanban.Factory.Create(NameKanban, Order);
};