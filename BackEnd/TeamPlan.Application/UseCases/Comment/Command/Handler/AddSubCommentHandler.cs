using MediatR;
using TeamPlan.Application.UseCases.Comment.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Comment.Command.Handler;

public class AddSubCommentHandler : HandlerBase,IRequestHandler<AddSubCommentRequest,Result>
{
    public AddSubCommentHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(AddSubCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdWithTaskAsync(request.CommentId);
        if (comment is null || comment.TaskId != request.TaskId)
            return new Error("Comment.NotFound", "not found!");

        var member = await _unitOfWork.MemberRepository.GetByPredicate(x => x.Id == request.MemberId);
        if (member is null)
            return new Error("Member.NotFound", "not found!");
        
        var createSubCommentResult = Domain.BackOffice.Entities.Comment.Factory
            .Create(comment.Task,member,request.Message);
        if (!createSubCommentResult.IsSuccess)
            return createSubCommentResult.Error;

        var subComment = createSubCommentResult.Value;
        subComment.AddCommentParent(comment);
        
        _unitOfWork.CommentRepository.Create(subComment);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}
