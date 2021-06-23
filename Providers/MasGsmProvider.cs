using EB2B.SMS.Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EB2B.SMS.Providers
{
    public class MasGsmProvider : ISmsProvider
    {
        //http://api.v2.masgsm.com.tr/docs
        private string _username { get; set; }
        private string _password { get; set; }
        private string _title { get; set; }


        public MasGsmProvider(string username, string password, string title)
        {
            this._username = username;
            this._password = password;
            this._title = title;
        }

        public async Task<double> GetCreditAsync()
        {
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                wb.Headers.Add("Authorization", _password);
                var response = wb.UploadValues("http://api.v2.masgsm.com.tr/v2/get/balance", "POST", data);
                double resp = double.Parse(Encoding.UTF8.GetString(response));
                return resp;
            }

        }

        public async Task SendAsync(string phonenumber, string messagecontent)
        {
            if (phonenumber is null)
                throw new ArgumentException("Lütfen geçerli bir telefon numarası giriniz");

            phonenumber = SmsHelper.ClearPhoneNumberText(phonenumber);

            if (phonenumber.Length != 11)
                throw new ArgumentException("Telefon numarası uyumlu değil : " + phonenumber);

            using (var wb = new WebClient())
            {
                var url = new Uri("http://api.v2.masgsm.com.tr/v2/sms/basic");
                var data = new NameValueCollection();

                data["originator"] = _title;
                data["message"] = messagecontent;
                data["encoding"] = "turkish"; // default , turkish , auto
                data["to"] = phonenumber;

                wb.Headers.Add("Authorization", _password);
                var response = wb.UploadValues(url, "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);
                Console.WriteLine(responseInString);
            }
        }
    }
}
