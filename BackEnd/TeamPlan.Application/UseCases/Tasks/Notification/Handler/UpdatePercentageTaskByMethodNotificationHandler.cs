using MediatR;
using TeamPlan.Application.UseCases.Tasks.Notification.Request;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Tasks.Notification.Handler;

public class UpdatePercentageTaskByMethodNotificationHandler
:INotificationHandler<UpdatePercentageTaskByMethodNotificationRequest>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePercentageTaskByMethodNotificationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePercentageTaskByMethodNotificationRequest request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByPredicate(x => x.Id == request.Task);
        if (task is null)
            return;
        task.AddPercentage((ushort)request.Percentage);
        _unitOfWork.TaskRepository.Update(task);
        await _unitOfWork.CommitAsync();
        
    }
}