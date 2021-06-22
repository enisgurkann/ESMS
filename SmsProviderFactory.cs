using EB2B.SMS.Providers;
using System;

namespace EB2B.SMS
{
    public class SmsProviderFactory : ISmsProviderFactory
    {
        public ISmsProvider Create(SmsTypes type, string username, string password, string title)
        {
            return type switch
            {
                SmsTypes.NETGSM => new NetGsmProvider(username, password, title),
                SmsTypes.MASGSM => new MasGsmProvider(username, password, title),
                SmsTypes.ILETIMERKEZI => new IletiMerkeziProvider(username, password, title),
                SmsTypes.SMSVITRINI => new SmsVitriniProvider(username, password, title),
                _ => throw new NotSupportedException("SMS Provider not found"),
            };
        }
    }
}
