using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Enterprises.Command.Request;

public record CreateEnterpriseRequest(string Name,Guid OwnerId): IRequest<Result>;