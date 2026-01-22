using MediatR;
using TeamPlan.Application.UseCases.Teams.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Query.handler;

internal class GetHistoryTaskByTeamHandler : HandlerBase,
    IRequestHandler<GetHistoryTaskByTeamRequest,Result<IEnumerable<Domain.BackOffice.Entities.Task>>>
{
    public GetHistoryTaskByTeamHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<IEnumerable<Domain.BackOffice.Entities.Task>>> Handle(GetHistoryTaskByTeamRequest request, CancellationToken cancellationToken)
    {
        request.Pagination.AlterPropertyIfInvalid();
        
        var tasks = await _unitOfWork
            .TaskRepository
            .GetTasksByTeamId(request.TeamId,request.Pagination.Skip,request.Pagination.Take);
        if (tasks is null)
            return new Error("Task.NotFound", "Not found");
        return Result<IEnumerable<Domain.BackOffice.Entities.Task>>.Success(tasks.OrderBy(x=>x.Priority));
    }
}