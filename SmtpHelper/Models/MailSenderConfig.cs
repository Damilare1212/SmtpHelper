namespace MA.SmtpHelper.Models;

public class MailSenderConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool UseSsl { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}