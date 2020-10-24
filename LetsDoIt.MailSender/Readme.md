![alt text](https://github.com/lets-do-it-pl/common/blob/main/LetsDoIt.MailSender/src/LetsDoIt.MailSender/icon.png)
# Let's Do IT - Mail Sender

![Mail Sender - Build](https://github.com/lets-do-it-pl/common/workflows/Mail%20Sender%20-%20Build/badge.svg?branch=main)
![Mail Sender - Deploy to nuget.org](https://github.com/lets-do-it-pl/common/workflows/Mail%20Sender%20-%20Deploy%20to%20nuget.org/badge.svg)

## Configuration
### 1. Add Mail server and mail user information to appsettings.json

```json
{
  ...
 "Smtp": {
    "Host": "<HOST_NAME>",
    "Port": <PORT_NAME>,
    "Email": "<EMAIL_ADDRESS>",
    "Username": "<USERNAME>",
    "Password": "<PASSWORD>"
  }
  ...
}
```

### 2. Register Smtp Options

```c#
  services.Configure<SmtpOptions>(Configuration.GetSection(SmtpOptions.SmtpSectionName));
```

### 3. Register Mail Sender

```c#
  services.AddMailSender();
```

### 4. Inject `IMailSender` and start using it
```c#
public class UserService
{
  private readonly IMailSender _mailSender;
  
  public UserService(IMailSender mailSender)
  {
    _mailSender = mailSender
  }
  
  public async Task SaveUserAsync(User user){
    ....
    
    // Async Usage
    await _mailSender.SendAsync("SUBJECT", "HTML_CONTENT", "TO_MAIL_1", "TO_MAIL_2", .....);
  }
  
  
  public void InformUser(User user){
    ....
    
    // Sync Usage
    _mailSender.Send("SUBJECT", "HTML_CONTENT", "TO_MAIL_1", "TO_MAIL_2", .....);
  }
}
```
