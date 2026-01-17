using MediatR;
using TeamPlan.Application.UseCases.Owners.Commands.Response;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Owners.Commands.Request;

public record LoginOwnerRequest(string Email,string Password) : IRequest<Result<OwnerAuthResponse>>;