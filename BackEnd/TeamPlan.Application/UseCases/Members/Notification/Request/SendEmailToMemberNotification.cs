using MediatR;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Members.Notification.Request;

public record SendEmailToMemberNotification(string Email, string Message,string Name,string Title)
    : INotification;