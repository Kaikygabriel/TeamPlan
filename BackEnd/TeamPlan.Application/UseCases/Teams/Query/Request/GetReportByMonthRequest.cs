using MediatR;
using TeamPlan.Application.DTOs.Tasks;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Teams.Query.Request;

public record GetReportByMonthRequest(Guid TeamId,int Month) : IRequest<Result<ReportTaskDto>>;