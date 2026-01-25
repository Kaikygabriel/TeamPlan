using MediatR;
using TeamPlan.Application.UseCases.Kanban.Command.Request;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Kanban.Command.Handler;

public class AddKanbanInTaskHandler : HandlerBase, IRequestHandler<AddKanbanInTaskRequest,Result>
{
    public AddKanbanInTaskHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result> Handle(AddKanbanInTaskRequest request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetByIdWithTasks(request.TeamId);
        if (team is null)
            return new Error("Team.NotFound", "Not Found!");
        
        var taskInTeam = team.Tasks.FirstOrDefault(x => x.Id == request.TaskId);
        if (taskInTeam is null)
            return new Error("TaskOrKanban.NotFound", "Not Found!");
        
        var resultGetKanban = team.GetIndexKanbanByTitle(request.TitleKanban);
        if (!resultGetKanban.IsSuccess)
            return resultGetKanban.Error;
        
        var kanbanIndex = resultGetKanban.Value;
        taskInTeam.AlterCurrentKanban((ushort)kanbanIndex);
        _unitOfWork.TaskRepository.Update(taskInTeam);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}