using Microsoft.Extensions.Configuration;
using MimeKit;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Interfaces.Services;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace TeamPlan.Application.Services;

internal class ServiceEmail : IServiceEmail
{
    private readonly IConfiguration _configuration;

    public ServiceEmail(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(EmailBuilder email)
    {
        var mensagem = new MimeMessage();
    
        mensagem.From.Add(new MailboxAddress("monetra", _configuration["EmailConfig:Email"]));
        
        mensagem.To.Add(new MailboxAddress(email.Name, email.ToAddress));
    
        mensagem.Subject = email.Title;
        mensagem.Body = new TextPart("html") { Text = email.Message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            
            await client.AuthenticateAsync(_configuration["EmailConfig:Email"], _configuration["EmailConfig:key"]);

            await client.SendAsync(mensagem);
            await client.DisconnectAsync(true);
        }        
    }
}