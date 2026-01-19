using MediatR;
using TeamPlan.Application.DTOs.Tasks;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using Task = TeamPlan.Domain.BackOffice.Entities.Task;

namespace TeamPlan.Application.UseCases.Tasks.Command.Request;

public record CreateTaskRequest(CreateTaskDto CreateTaskDto ,Guid? MemberId)
    : IRequest<Result>
{
    public Result<Task> ToEntity()
        => Task.Factories.Create
        (
            CreateTaskDto.EndDate, 
            CreateTaskDto.Title,
            CreateTaskDto.Description,
            CreateTaskDto.TeamId,
            CreateTaskDto.Priority
        );
}