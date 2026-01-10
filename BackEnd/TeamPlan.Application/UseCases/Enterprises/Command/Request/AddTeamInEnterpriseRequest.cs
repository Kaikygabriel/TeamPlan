using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Enterprises.Command.Request;

public record AddTeamInEnterpriseRequest(Guid OwnerId,Guid ManageId,string NameTeam,Guid EnterpriseId) 
    : IRequest<Result>;