using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Enterprises.Command.Request;

public record RemoveTeamRequest(Guid TeamI, Guid EnterpriseId,Guid OwnerId)
    : IRequest<Result>;