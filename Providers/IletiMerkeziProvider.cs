using EB2B.SMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EB2B.SMS.Providers
{
    public class IletiMerkeziProvider : ISmsProvider
    {
        //https://a2psmsapi.com/?ref=iletimerkezicom#apidoc
        private string _username { get; set; }
        private string _password { get; set; }
        private string _title { get; set; }


        public IletiMerkeziProvider(string username, string password, string title)
        {
            this._username = username;
            this._password = password;
            this._title = title;

        }

        public async Task SendAsync(string phonenumber, string messagecontent)
        {
            if (phonenumber is null)
                throw new ArgumentException("Lütfen geçerli bir telefon numarası giriniz");

            phonenumber = SmsHelper.ClearPhoneNumberText(phonenumber);

            if (phonenumber.Length != 11)
                throw new ArgumentException("Telefon numarası uyumlu değil : " + phonenumber);

            string getData = await SmsHelper.Get($"https://api.iletimerkezi.com/v1/send-sms/get/?username={_username}&password={_password}&text={messagecontent}&receipents={phonenumber}&sender={_title}");
            if (getData == "-1")
                throw new ArgumentException("Servis Hatası");
        }

        public async Task<double> GetCreditAsync()
        {
            return 0;
        }
    }
}
