using MediatR;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Members.Notification.Request;

public record SendEmailToMemberNotification(EmailBuilder EmailBuilder)
    : INotification;