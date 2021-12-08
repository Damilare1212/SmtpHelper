namespace MA.SmtpHelper;

public interface IMailFrom
{
    public IMailTo From(string email, string name);
}