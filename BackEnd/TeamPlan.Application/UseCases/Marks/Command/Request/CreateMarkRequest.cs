using MediatR;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Application.UseCases.Marks.Command.Request;

public record CreateMarkRequest(string Title, string Descriptor, ushort TaskCount,Guid TeamId)
    : IRequest<Result>
{
    public Result<Mark> ToEntity()
        => Mark.Factory.Create(Title, Descriptor, TaskCount,TeamId);
}