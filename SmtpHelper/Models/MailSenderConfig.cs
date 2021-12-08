// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace MA.SmtpHelper.Models;

public class MailSenderConfiguration
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public bool UseSsl { get; set; }
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
}