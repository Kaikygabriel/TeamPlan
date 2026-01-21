using MediatR;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Comment.Command.Request;

public record CreateCommentRequest(Guid TaskId, Guid MemberId, string Message) : IRequest<Result>
{
 
}