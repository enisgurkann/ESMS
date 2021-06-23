using EB2B.SMS;
using EB2B.SMS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMS.Providers
{
    public class JetGsmProvider : ISmsProvider
    {
        private string _username { get; set; }
        private string _password { get; set; }
        private string _title { get; set; }
        private const string apiUrl = "https://ws.jetsms.com.tr/api/";

        public SmsVitriniProvider(string username, string password, string title)
        {
            this._username = username;
            this._password = password;
            this._title = title;
        }
        public Task<double> GetCreditAsync()
        {
            return Task.FromResult(double.Parse("0"));
        }

        public async Task SendAsync(string phonenumber, string messagecontent)
        {
            if (phonenumber is null)
                throw new ArgumentException("Lütfen geçerli bir telefon numarası giriniz");

            phonenumber = SmsHelper.ClearPhoneNumberText(phonenumber);

            if (phonenumber.Length != 11)
                throw new ArgumentException("Telefon numarası uyumlu değil : " + phonenumber);


            string getData = await SmsHelper.Post($"https://api.jetsms.com.tr/SMS-Web/HttpSmsSend?Password={_password}&Username={_username}&Msisdns={phonenumber}&Messages={messagecontent}&Originator={_title}",null);

            string getCode = getData.Split(' ')[0].ToString().Split('=')[1];
            if (getCode == "-5")
                throw new ArgumentException("Login hatası: Username, Password, Orginator uyumsuzluğu");
            else if (getCode == "-6")
                throw new ArgumentException("Girilen bir kısım veride hata oluştu");
            else if (getCode == "-7")
                throw new ArgumentException("SendDate bugünden büyük ve geçerli bir tarih olmalıdır");
            else if (getCode == "-8")
                throw new ArgumentException("En azından bir Msisdn bilgisi verilmelidir");
            else if (getCode == "-9")
                throw new ArgumentException("En azından bir Message değeri verilmelidir");
            else if (getCode == "-10")
                throw new ArgumentException("Birden fazla Msisdn e farklı mesaj gönderimi için, Msisdn ve Message sayıları aynı olmadır");
            else if (getCode == "-15")
                throw new ArgumentException("Sistem hatası");
            else if (getCode == "-99")
                throw new ArgumentException("Bilinmeyen Hata");

        }

    }
}
