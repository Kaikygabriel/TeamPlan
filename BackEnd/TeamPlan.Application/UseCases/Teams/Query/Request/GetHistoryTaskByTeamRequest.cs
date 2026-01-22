using MediatR;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Teams.Query.Request;

public record GetHistoryTaskByTeamRequest(Guid TeamId,QueryPagination Pagination) : IRequest<Result<IEnumerable<Domain.BackOffice.Entities.Task>>>;