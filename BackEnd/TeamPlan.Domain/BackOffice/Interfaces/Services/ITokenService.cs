using System.Security.Claims;

namespace TeamPlan.Domain.BackOffice.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim>claims);
    string GetRoleByToken(string token);
}