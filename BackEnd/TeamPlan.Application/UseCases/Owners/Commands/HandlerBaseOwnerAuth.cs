using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.UseCases.Owners.Commands;

public abstract class HandlerBaseOwnerAuth : HandlerBase
{
    protected readonly IUserServiceAuth UserServiceAuth;
    protected readonly ITokenService TokenService;

    public HandlerBaseOwnerAuth(IUnitOfWork unitOfWork, ITokenService tokenService, IUserServiceAuth userServiceAuth) :
        base(unitOfWork)
    {
        TokenService = tokenService;
        UserServiceAuth = userServiceAuth;
    }

    protected string GenerateAcessTokenByOwner(Owner owner)
    {
        var claims = GenerateClaimsByOwner(owner);
        return TokenService.GenerateAccessToken(claims);
    }


    private IEnumerable<Claim> GenerateClaimsByOwner(Owner owner)
        => new List<Claim>()
        {
            new Claim(ClaimTypes.Email, owner.Name),
            new Claim(ClaimTypes.Email, owner.User.Email.Address),
            new Claim(ClaimTypes.Role, Roles.Owner),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
}