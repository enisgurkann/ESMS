using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EB2B.SMS
{
    public interface ISmsProvider
    {
        Task SendAsync(string phonenumber, string messagecontent);
        Task<double> GetCreditAsync();
    }
}
