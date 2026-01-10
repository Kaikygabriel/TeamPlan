using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Teams.Command.Request;

public record AddMemberInTeamRequest(Guid TeamId,Guid ManagerId,string EmailMember)  : IRequest<Result>;