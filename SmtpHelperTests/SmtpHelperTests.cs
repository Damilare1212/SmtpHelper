using Xunit;
using MA.SmtpHelper;
using MA.SmtpHelper.Models;
using Microsoft.Extensions.Configuration;

namespace SmtpHelperTests;

public class SmtpHelper
{
    private readonly MailSenderConfiguration _mailSenderConfiguration;

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

    private IMailContent GetMailHeader()
    {
        return MailSender
            .Connect(_mailSenderConfiguration)
            .From("test@test.com", "Test")
            .SendTo("test1@test.com", "Test 1")
            .Done();
    }
    
    private IMailContent AlternativeGetMailHeader()
    {
        return MailSender
            .Connect(_mailSenderConfiguration)
            .From("another@test.com", "Another")
            .SendTo("another1@test.com", "Another 1")
            .Done();
    }
    
    [Fact(DisplayName = "Simple send test")]
    public void TestSend()
    {
        GetMailHeader()
            .Subject("Test")
            .Body("Test content", false)
            .Send();
    }
    
    [Fact(DisplayName = "Send Html body send test")]
    public void TestHtmlSend()
    {
        GetMailHeader()
            .Subject("Test HTML")
            .Body("<h1>Test h1</h1> <strong>Test<strong>", true)
            .Send();
    }
    
    [Fact(DisplayName = "Multiple sending methods")]
    public void TestMultipleSenders()
    {
        GetMailHeader()
            .Subject("Test")
            .Body("", false)
            .Send();
        
        AlternativeGetMailHeader()
            .Subject("Test 2")
            .Body("", false)
            .Send();
    }
}