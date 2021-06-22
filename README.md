![alt text](https://github.com/enisgurkann/ESMS/blob/master/logo.PNG?raw=true)

# ESMS - Multi Sms Provider

.NetCore

## SmsProvider Usage

```
PM> Install-Package ESms
```

```csharp
services.AddSingleton<ISmsProviderFactory, SmsProviderFactory>();
 ```
 
 
```
PM> Injection
```


```csharp
  private readonly ISmsProvider _smsProvider;
  public SmsController(ISmsProviderFactory SmsService)
  {
      _smsProvider = smsProvider.Create(SmsTypes.NETGSM, "Username", "Password", "TITLE");
  }
```

```
PM> Using
```
```csharp
   public async Task SendSms(string phonenumber, string messagecontent) => await _smsProvider.SendAsync(phonenumber, messagecontent);
   
   public async Task<double> GetCredit()  => await _smsProvider.GetCreditAsync();
    
```


 
