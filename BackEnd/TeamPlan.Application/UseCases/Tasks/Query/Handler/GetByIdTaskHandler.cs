using MediatR;
using TeamPlan.Application.DTOs.StoreFront.Tasks;
using TeamPlan.Application.UseCases.Tasks.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using Task = TeamPlan.Domain.BackOffice.Entities.Task;

namespace TeamPlan.Application.UseCases.Tasks.Query.Handler;

internal class GetByIdTaskHandler : HandlerBase,IRequestHandler<GetByIdTaskRequest,Result<TaskDashBoardDto>>
{
    public GetByIdTaskHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<TaskDashBoardDto>> Handle(GetByIdTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdWithTeamWithMember(request.TaskId);
        if (task is null)
            return Result<TaskDashBoardDto>.Failure(new("Task.NotFound", "Not found"));
        if(task.TeamId != request.TeamId)
            return Result<TaskDashBoardDto>.Failure(new("Team.NotFound", "Not found"));
        var kanbanInTask = task.Team.Kanbans.ElementAtOrDefault((int)task.KanbanCurrent);
        var response = ConvertInTaskDto(task, kanbanInTask?.Title);
        
        return Result<TaskDashBoardDto>.Success(response);
    }
    
    private TaskDashBoardDto ConvertInTaskDto(Task task,string kanban)
        => new TaskDashBoardDto(
            task.Title, 
            task.Description,
            task.CreateAt,
            task.EndDate,
            task.Member?.Name,
            task.Percentage,
            task.Team.Name,
            kanban,
            task.Team.Kanbans.Select(x=>x.Title),
            task.Comments.OrderByDescending(x=>x.CreateAt));
}