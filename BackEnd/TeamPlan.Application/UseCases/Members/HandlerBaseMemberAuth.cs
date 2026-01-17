using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.UseCases.Members;

internal class HandlerBaseMemberAuth : HandlerBase
{
    protected readonly ITokenService TokenService;

    public HandlerBaseMemberAuth(IUnitOfWork unitOfWork, ITokenService tokenService) :
        base(unitOfWork)
    {
        TokenService = tokenService;
    }

    protected string GenerateAcessTokenByMember(Member owner)
    {
        var claims = GenerateClaimsMember(owner);
        return TokenService.GenerateAccessToken(claims);
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