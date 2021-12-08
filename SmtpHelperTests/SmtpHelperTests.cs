using Xunit;
using MA.SmtpHelper;
using MA.SmtpHelper.Models;
using Microsoft.Extensions.Configuration;

namespace SmtpHelperTests;

public class SmtpHelper
{
    private MailSenderConfiguration _mailSenderConfiguration;

    public SmtpHelper()
    {
        IConfigurationRoot? configuration = new ConfigurationBuilder()
            .AddUserSecrets<SmtpHelper>()
            .Build();

        _mailSenderConfiguration = new MailSenderConfiguration()
        {
            Host = configuration["Smtp:Host"],
            User = configuration["Smtp:User"],
            Password = configuration["Smtp:Password"],
            Port = 587,
            UseSsl = false
        };
    }
    
    [Fact(DisplayName = "Simple send test")]
    public void TestSend()
    {
        MailSender
            .Connect(_mailSenderConfiguration)
            .From("test@test.com", "Test")
            .SendTo("test1@test.com", "Test 1")
            .Done()
            .Subject("Test")
            .Body("Test content", false)
            .Send();
    }
}