namespace TeamPlan.Domain.BackOffice.Interfaces.Services;

public interface IServiceEmail
{
    Task SendEmailAsync(string email, string message,string name,string title);
}