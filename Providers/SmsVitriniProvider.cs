using EB2B.SMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EB2B.SMS.Providers
{
    public class SmsVitriniProvider : ISmsProvider
    {
        //http://www.smsvitrini.com.tr/cozumler/api
        private string _username { get; set; }
        private string _password { get; set; }
        private string _title { get; set; }


        public SmsVitriniProvider(string username, string password, string title)
        {
            this._username = username;
            this._password = password;
            this._title = title;
        }

        public async Task SendAsync(string phonenumber, string messagecontent)
        {
            string getData = await SmsHelper.Post("http://api.smsvitrini.com/index.php", $"islem=1&user={_username}&pass={_password}&mesaj={messagecontent}&numaralar={phonenumber}&baslik={_title}");
            if (getData == "-1")
                throw new ArgumentException("Servis Hatası");
        }

        public async Task<double> GetCreditAsync()
        {
            string getData = await SmsHelper.Post("http://api.smsvitrini.com/index.php", $"islem=2&user={_username}&pass={_password}");

            string getCode = getData.Split(' ')[0].ToString();
            double Amount = 0;
            if (getCode == "00")
            {
                Amount = double.Parse(getData.Split(' ')[1].ToString());
            }

            return Amount;
        }
    }
}
