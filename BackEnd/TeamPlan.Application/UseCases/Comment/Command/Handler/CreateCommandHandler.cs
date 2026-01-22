using MediatR;
using TeamPlan.Application.UseCases.Comment.Command.Request;
using TeamPlan.Application.UseCases.Members.Notification.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Comment.Command.Handler;

internal class CreateCommandHandler : HandlerBase,IRequestHandler<CreateCommentRequest,Result>
{
    private readonly IMediator _mediator;
    public CreateCommandHandler(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork)
    {
        _mediator = mediator;
    }

    public async Task<Result> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByPredicate(x => x.Id == request.TaskId);
        if(task is null)
            return new Error("Task.NotFound", "Not Found ! ");
        
        var member = await _unitOfWork.MemberRepository.GetByPredicate(x => x.Id == request.MemberId);
        if(member is null || member.TeamId != task.TeamId)
            return new Error("Member.NotFound", "Not Found ! ");
        
        var commentResultCreate = Domain.BackOffice.Entities.Comment.Factory.Create
            (task, member, request.Message);
        if (!commentResultCreate.IsSuccess)
            return commentResultCreate.Error;

        var comment = commentResultCreate.Value;

        var methodsInComment = TryToGetMethodOfCommentOrNull(comment.Message);

        if (methodsInComment is not null)
            await SendMethodInComment(methodsInComment);
        
        task.AddComment(comment);
        _unitOfWork.CommentRepository.Create(comment);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }

    private IEnumerable<string>? TryToGetMethodOfCommentOrNull(string commentMessage)
    {
        var methods = commentMessage
            .Trim()
            .Split()
            .Where(x=>x.Contains('@') && x.Contains(':') && (x.Contains("User") || x.Contains("Task")));
        if (methods is null)
            return null;
        return methods;
    }
    private async Task SendMethodInComment(IEnumerable<string> methods)
    {
        foreach (var method in methods)
        {
            if (method.Contains("User"))
            {
                var email = method.Remove(0,6);
                await _mediator.Publish(new SendEmailToMemberNotification(
                    email, Emails.CommentMethod(email), email, "Voce foi mencionado "));
            }

            if (method.Contains("Task"))
            {
                //ainda em produção
            }
        }
    }
}