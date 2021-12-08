# Smtp Helper

Utility used to easily send mails using a SMTP server.

## Use

Just call `MailSender.Connect([config])` and follow the chain until send().

Example (simple content):

```c#
_mailSenderConfiguration = new MailSenderConfiguration()
{
    Host = configuration["Smtp:Host"],
    User = configuration["Smtp:User"],
    Password = configuration["Smtp:Password"],
    Port = 587,
    UseSsl = false
};

MailSender
    .Connect(_mailSenderConfiguration)
    .From("test@test.com", "Test")
    .SendTo("test1@test.com", "Test 1")
    .Done()
    .Body("Test content", false)
    .Send();
```