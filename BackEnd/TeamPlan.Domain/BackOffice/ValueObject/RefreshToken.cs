using TeamPlan.Domain.BackOffice.Commum;

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
    public DateTime DateExpired{ get; init; }
    
    public static class Factory
    {
        public static Result<RefreshToken> Create(string token)
        {
            return Result<RefreshToken>.Success(new RefreshToken(token));
        }
    }
}