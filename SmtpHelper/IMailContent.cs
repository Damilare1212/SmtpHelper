namespace MA.SmtpHelper;

public interface IMailContent
{
    public IMailBody Subject(string? subject);
}