using Microsoft.Extensions.Configuration;
using MimeKit;
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

    public async Task SendEmailAsync(string email, string message,string name,string title)
    {
        var mensagem = new MimeMessage();
    
        mensagem.From.Add(new MailboxAddress("monetra", _configuration["EmailConfig:Email"]));
        
        mensagem.To.Add(new MailboxAddress(email, email));
    
        mensagem.Subject = title;
        mensagem.Body = new TextPart("html") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            
            await client.AuthenticateAsync(_configuration["EmailConfig:Email"], _configuration["EmailConfig:key"]);

            await client.SendAsync(mensagem);
            await client.DisconnectAsync(true);
        }        
    }
}