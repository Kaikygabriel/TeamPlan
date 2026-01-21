using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Comment.Command.Request;

public record AddSubCommentRequest(Guid CommentId, Guid TaskId, Guid MemberId, string Message):IRequest<Result>;