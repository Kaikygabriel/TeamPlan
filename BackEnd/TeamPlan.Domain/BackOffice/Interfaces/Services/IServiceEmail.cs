using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Domain.BackOffice.Interfaces.Services;

public interface IServiceEmail
{
    Task SendEmailAsync(EmailBuilder email);
}