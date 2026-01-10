using MediatR;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Application.UseCases.Teams.Command.Request;

public record CreateTeamRequest(string Name,Member Manage,Guid Enterprise) : IRequest<Result<Team>>; 