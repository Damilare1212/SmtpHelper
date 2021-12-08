namespace MA.SmtpHelper;

public interface IMailBody
{
    public IMailSender Body(string? body, bool isHtml);
}