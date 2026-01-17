using MediatR;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.Teams.Command.Request;

public record RemoveMemberInTeamRequest(Guid TeamId,Guid ManagerId,Guid IdMemberRemove): IRequest<Result>;