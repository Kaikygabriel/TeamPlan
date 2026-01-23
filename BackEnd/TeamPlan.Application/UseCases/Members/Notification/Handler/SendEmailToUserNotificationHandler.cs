using MediatR;
using TeamPlan.Application.UseCases.Members.Notification.Request;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.UseCases.Members.Notification.Handler;

public class SendEmailToUserNotificationHandler: INotificationHandler<SendEmailToMemberNotification>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceEmail _serviceEmail;

    public SendEmailToUserNotificationHandler(IServiceEmail serviceEmail, IUnitOfWork unitOfWork)
    {
        _serviceEmail = serviceEmail;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SendEmailToMemberNotification notification, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetByEmail(notification.EmailBuilder.ToAddress);
        if (member is null)
            return;
        
        await _serviceEmail.SendEmailAsync(notification.EmailBuilder);
    }
}