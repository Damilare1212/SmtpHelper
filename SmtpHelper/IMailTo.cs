using MimeKit;

namespace MA.SmtpHelper;

public interface IMailTo
{
    public IMailTo SendTo(string email, string name);
    public IMailTo SendToList(IEnumerable<MailboxAddress> emails);
    public IMailTo SendToBcc(string email, string name);
    public IMailTo SendToListBcc(IEnumerable<MailboxAddress> emails);
    public IMailContent Done();
}