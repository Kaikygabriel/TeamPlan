using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using TeamPlan.Application.UseCases.Members.Command.Request;
using TeamPlan.Application.UseCases.Members.Command.Response;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;
using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Application.UseCases.Members.Command.Handler;

internal sealed class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest,Result<AuthMemberResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public RefreshTokenHandler(ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthMemberResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetByUserIdWithUser(request.UserId);
        if (member is null)
            return Result<AuthMemberResponse>.Failure(new Error("Member.NotFound", "Member not found !"));
        if(!member.User.RefreshToken.EqualsToken(request.RefreshToken))
            return Result<AuthMemberResponse>.Failure(new Error("Token.Invalid", "Token Invalid! "));

        var resultCreateRefreshToken = RefreshToken.Factory.Create(_tokenService.GenerateRefreshToken());
        if (!resultCreateRefreshToken.IsSuccess) return resultCreateRefreshToken.Error;
        
        var refreshToken = resultCreateRefreshToken.Value;
        var token = _tokenService.GenerateAccessToken(GenerateClaimsMember(member));
        member.User.AddRefreshToken(refreshToken);

        await _unitOfWork.CommitAsync();
        var result = new AuthMemberResponse(token, member.User.Id, refreshToken.Token);
        return Result<AuthMemberResponse>.Success(result);
    }
    
    private IEnumerable<Claim> GenerateClaimsMember(Member member)
        => new List<Claim>()
        {
            new Claim(ClaimTypes.Email, member.Name),
            new Claim(ClaimTypes.Email, member.User.Email.Address),
            new Claim(ClaimTypes.Role, Roles.Member),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
}