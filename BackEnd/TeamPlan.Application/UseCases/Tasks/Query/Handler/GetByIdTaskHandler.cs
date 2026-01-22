using MediatR;
using TeamPlan.Application.DTOs.Tasks;
using TeamPlan.Application.UseCases.Tasks.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Tasks.Query.Handler;

internal class GetByIdTaskHandler : HandlerBase,IRequestHandler<GetByIdTaskRequest,Result<GetTaskDto>>
{
    public GetByIdTaskHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<GetTaskDto>> Handle(GetByIdTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdWithTeamWithMember(request.TaskId);
        if (task is null)
            return Result<GetTaskDto>.Failure(new("Task.NotFound", "Not found"));
        if(task.TeamId != request.TeamId)
            return Result<GetTaskDto>.Failure(new("Team.NotFound", "Not found"));
        var response = new GetTaskDto(task.Title, task.Description, task.CreateAt, task.EndDate,
            task.Member?.Name,task.Percentage,task.Team.Name,task.Comments.OrderByDescending(x=>x.CreateAt));
        return Result<GetTaskDto>.Success(response);
    }
}