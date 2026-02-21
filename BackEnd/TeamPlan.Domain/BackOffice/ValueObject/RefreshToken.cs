using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Domain.BackOffice.ValueObject;

public class RefreshToken
{
    private RefreshToken()
    {
        
    }
    private RefreshToken(string token)
    {
        Token = token;
        DateExpired = DateTime.UtcNow.AddDays(1);
    }
    public string Token { get; init; }
    public DateTime? DateExpired{ get; init; }

    public bool EqualsToken(string otherToken)
    {
        if (Token is null)
            return false;
        return Token.Equals(otherToken);
    } 
    public static class Factory
    {
        public static Result<RefreshToken> Create(string token)
        {
            if (VerifyToken(token))
                return new Error("token.invalid","Token invalid !");
            return Result<RefreshToken>.Success(new RefreshToken(token));
        }
    }
    private static bool VerifyToken(string token)
        => string.IsNullOrWhiteSpace(token) || token.Length <= 5;

}