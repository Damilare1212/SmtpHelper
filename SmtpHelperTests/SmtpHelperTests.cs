using Xunit;
using MA.SmtpHelper;
using Microsoft.Extensions.Configuration;

namespace SmtpHelperTests;

public class SmtpHelper
{
    public SmtpHelper()
    {
        IConfigurationRoot? configuration = new ConfigurationBuilder()
            .AddUserSecrets<SmtpHelper>()
            .Build();
    }
    
    [Fact]
    public void TestSend()
    {
        
    }
}