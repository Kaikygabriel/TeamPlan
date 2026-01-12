using MediatR;
using TeamPlan.Application.UseCases.Members.Command.Response;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Members.Command.Request;

public record LoginMemberRequest(string Email,string Password) : IRequest<Result<AuthMemberResponse>>;