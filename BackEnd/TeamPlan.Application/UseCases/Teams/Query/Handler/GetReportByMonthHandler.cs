using MediatR;
using TeamPlan.Application.DTOs.Tasks;
using TeamPlan.Application.UseCases.Teams.Query.Request;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;

namespace TeamPlan.Application.UseCases.Teams.Query.handler;

internal class GetReportByMonthHandler : HandlerBase,IRequestHandler<GetReportByMonthRequest,Result<ReportTaskDto>>
{
    public GetReportByMonthHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<Result<ReportTaskDto>> Handle(GetReportByMonthRequest request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.TeamRepository.GetTasksInMonthByTeamId(request.TeamId);
        if(tasks is null)
            return new Error("Task.NotFound", "Not Found");
        var tasksAverageInMonth = tasks!.Count();

        return Result<ReportTaskDto>.Success(new  (tasksAverageInMonth,tasks!));
    }
}