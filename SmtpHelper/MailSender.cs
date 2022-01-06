using MA.SmtpHelper.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace MA.SmtpHelper;

public class MailSender : IMailFrom, IMailTo, IMailContent, IMailBody, IMailSender
{
    private readonly string _host;
    private readonly int _port;
    private readonly bool _useSsl;
    private readonly string _user;
    private readonly string _password;

    private MailboxAddress? _from;
    private IList<MailboxAddress> _tos = new List<MailboxAddress>();
    private IList<MailboxAddress> _bccs = new List<MailboxAddress>();

    private string? _subject;
    private string? _body;
    private bool _isHtml;

    private MailSender(string host, int port, bool useSsl, string user, string password)
    {
        _host = host;
        _port = port;
        _useSsl = useSsl;
        _user = user;
        _password = password;
    }

    private void SetMailContent(MimeMessage message)
    {
        message.Subject = _subject;
        string mime = _isHtml ? "html" : "plain";
        message.Body = new TextPart(mime)
        {
            Text = _body
        };
    }

    private void SetMailBccs(MimeMessage message)
    {
        foreach (MailboxAddress bcc in _bccs)
        {
            message.Bcc.Add(bcc);
        }
    }

    private void SetMailTos(MimeMessage message)
    {
        foreach (MailboxAddress to in _tos)
        {
            message.To.Add(to);
        }
    }

    private void SendAndDisconnect(MimeMessage message)
    {
        using var client = new SmtpClient();
        client.Connect(_host, _port, _useSsl);
        client.Authenticate(_user, _password);
        client.Send(message);
        client.Disconnect(true);
    }

    public static IMailFrom Connect(MailSenderConfiguration configuration)
    {
        return new MailSender
        (
            configuration.Host,
            configuration.Port,
            configuration.UseSsl,
            configuration.User,
            configuration.Password
        );
    }

    public IMailTo From(string email, string name)
    {
        _from = new MailboxAddress(name, email);
        return this;
    }

    public IMailTo SendTo(string email, string name)
    {
        var to = new MailboxAddress(name, email);
        _tos.Add(to);
        return this;
    }

    public IMailTo SendToList(MailboxAddress[] emails)
    {
        _tos = _tos.Concat(emails).ToList();
        return this;
    }

    public IMailTo SendToBcc(string email, string name)
    {
        var bcc = new MailboxAddress(name, email);
        _bccs.Add(bcc);
        return this;
    }

    
    public IMailTo SendToListBcc(params MailboxAddress[] emails)
    {
        _bccs = _bccs.Concat(emails).ToList();
        return this;
    }

    public IMailContent Done()
    {
        return this;
    }

    public IMailBody Subject(string? subject)
    {
        _subject = subject;
        return this;
    }

    public IMailSender Body(string? body, bool isHtml)
    {
        _body = body;
        _isHtml = isHtml;
        return this;
    }

    public void Send()
    {
        var message = new MimeMessage();
        message.From.Add(_from);
        SetMailTos(message);
        SetMailBccs(message);
        SetMailContent(message);
        SendAndDisconnect(message);
    }
}