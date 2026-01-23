using MediatR;
using TeamPlan.Application.UseCases.Comment.Command.Notification.Request;
using TeamPlan.Application.UseCases.Members.Notification.Request;
using TeamPlan.Application.UseCases.Tasks.Notification;
using TeamPlan.Application.UseCases.Tasks.Notification.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;
using Task = System.Threading.Tasks.Task;

namespace TeamPlan.Application.UseCases.Comment.Command.Notification.Handler;

public class CreateCommentNotificationHandler : INotificationHandler<CreateCommentNotificationRequest>
{
    private readonly IMediator _mediator;

    public CreateCommentNotificationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(CreateCommentNotificationRequest notification, CancellationToken cancellationToken)
    {
        var methodsInComment = TryToGetMethodOfCommentOrNull(notification.Comment.Message);

        if (methodsInComment is not null)
            await SendMethodInComment(methodsInComment,notification.Comment.Message,notification.Task,notification.Member,cancellationToken);
    }
    
    private IEnumerable<string>? TryToGetMethodOfCommentOrNull(string commentMessage)
    {
        var methods = commentMessage
            .Trim()
            .Split()
            .Where(x=> x.Contains("@User:",StringComparison.InvariantCultureIgnoreCase) ||
                       x.Contains("@Task:Percentage=",StringComparison.InvariantCultureIgnoreCase));
        if (!methods.Any())
            return null;
        return methods;
    }
    
    
    private async Task SendMethodInComment(IEnumerable<string> methods,string comment,TeamPlan.Domain.BackOffice.Entities.Task task,Member member,CancellationToken ct = default)
    {
        foreach (var method in methods)
        {
            if (method.Contains("User",StringComparison.InvariantCultureIgnoreCase))
            {
                var email = method.Remove(0,6).Trim().Trim(',','(',')','[',']');

                await _mediator.Publish(
                    new SendEmailToMemberNotification
                        (
                            EmailBuilder.Configuration()
                                .To(email)
                                .WithMessage(EmailMessage.CommentMethod(comment))
                                .WithName( member.Name)
                                .WithTitle("Menção :  ")
                                .Build()
                            ),ct
                );
            }

            if (method.Contains("Task:Percentage=",StringComparison.InvariantCultureIgnoreCase))
            {
                var percentageText = method.Remove(0,17).Trim(',');
                if (!int.TryParse(percentageText, out var percentage))
                    continue;
                
                if (percentage > 100)
                    percentage = 100;
                if (percentage < 0)
                    percentage = 0;
                await _mediator.Publish(new UpdatePercentageTaskByMethodNotificationRequest(task.Id, percentage),ct);
            }
        }
    }
}