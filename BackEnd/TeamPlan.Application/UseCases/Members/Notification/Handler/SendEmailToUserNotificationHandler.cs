using MediatR;
using TeamPlan.Application.UseCases.Members.Notification.Request;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.UseCases.Members.Notification.Handler;

public class SendEmailToUserNotificationHandler: INotificationHandler<SendEmailToMemberNotification>
{
    private readonly IServiceEmail _serviceEmail;

    public SendEmailToUserNotificationHandler(IServiceEmail serviceEmail)
    {
        _serviceEmail = serviceEmail;
    }

    public async Task Handle(SendEmailToMemberNotification notification, CancellationToken cancellationToken)
    {
        await _serviceEmail.SendEmailAsync(notification.Email, notification.Message,notification.Name,notification.Message);
    }
}