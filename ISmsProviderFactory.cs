using System;
using System.Collections.Generic;
using System.Text;

namespace EB2B.SMS
{
    public interface ISmsProviderFactory
{
        ISmsProvider Create(SmsTypes type, string Username, string Password,string Title);
    }
}
