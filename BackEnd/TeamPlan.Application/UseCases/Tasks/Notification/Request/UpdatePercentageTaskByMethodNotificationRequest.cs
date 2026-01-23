using MediatR;

namespace TeamPlan.Application.UseCases.Tasks.Notification.Request;

public record UpdatePercentageTaskByMethodNotificationRequest(Guid Task,int Percentage) : INotification;