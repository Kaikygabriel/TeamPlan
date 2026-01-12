using MediatR;
using TeamPlan.Application.UseCases.Members.Command.Request;
using TeamPlan.Application.UseCases.Members.Command.Response;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.UseCases.Members.Command.Handler;

internal class LoginMemberHandler : HandlerBaseMemberAuth,IRequestHandler<LoginMemberRequest,Result<AuthMemberResponse>>
{
    private IUserServiceAuth _auth;
    public LoginMemberHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUserServiceAuth auth) : base(unitOfWork, tokenService)
    {
        _auth = auth;
    }

    public async Task<Result<AuthMemberResponse>> Handle(LoginMemberRequest request, CancellationToken cancellationToken)
    {
        var resultUser = await _auth.VerifyUserIsValid(request.Email, request.Password);
        if (!resultUser.IsSuccess)
            return Result<AuthMemberResponse>.Failure(resultUser.Error);
        var member = await _unitOfWork.MemberRepository.GetByEmail(request.Email);

        var token = GenerateAcessTokenByMember(member);
        var response = new AuthMemberResponse(token, member.Id);
        return Result<AuthMemberResponse>.Success(response);
    }
}