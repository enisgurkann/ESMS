![alt text](https://github.com/enisgurkann/ESMS/blob/master/logo.PNG?raw=true)

# ESMS - Multi Sms Provider


Factory pattern structure written with .net5 for sms services

[![GitHub](https://img.shields.io/github/license/enisgurkann/ESMS?color=594ae2&logo=github&style=flat-square)](https://github.com/enisgurkann/ESMS/blob/master/LICENSE)
[![GitHub Repo stars](https://img.shields.io/github/stars/enisgurkann/ESMS?color=594ae2&style=flat-square&logo=github)](https://github.com/enisgurkann/ESMS/stargazers)
[![GitHub last commit](https://img.shields.io/github/last-commit/enisgurkann/ESMS?color=594ae2&style=flat-square&logo=github)](https://github.com/mudblazor/mudblazor)
[![Contributors](https://img.shields.io/github/contributors/enisgurkann/ESMS?color=594ae2&style=flat-square&logo=github)](https://github.com/enisgurkann/ESMS/graphs/contributors)
[![Discussions](https://img.shields.io/github/discussions/enisgurkann/ESMS?color=594ae2&logo=github&style=flat-square)](https://github.com/enisgurkann/ESMS/discussions)
[![Nuget version](https://img.shields.io/nuget/v/ESMS?color=ff4081&label=nuget%20version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/ESMS/)
[![Nuget downloads](https://img.shields.io/nuget/dt/ESMS?color=ff4081&label=nuget%20downloads&logo=nuget&style=flat-square)](https://www.nuget.org/packages/ESMS/)

 NETGSM,SMSVITRINI,ILETIMERKEZI,MASGSM

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


 
