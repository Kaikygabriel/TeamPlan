using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Enterprises.Command.Request;

public record AddTeamInEnterpriseRequest(Guid OwnerId,string EmailManager,string NameTeam,Guid EnterpriseId) 
    : IRequest<Result>;