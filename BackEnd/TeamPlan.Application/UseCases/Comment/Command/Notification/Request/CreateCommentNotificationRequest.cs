using MediatR;
using TeamPlan.Domain.BackOffice.Entities;
using Task = TeamPlan.Domain.BackOffice.Entities.Task;

namespace TeamPlan.Application.UseCases.Comment.Command.Notification.Request;

public record CreateCommentNotificationRequest(Domain.BackOffice.Entities.Comment Comment,Member Member,Task Task)
    : INotification;