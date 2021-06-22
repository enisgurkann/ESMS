using EB2B.SMS.Helper;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EB2B.SMS.Providers
{
    public class NetGsmProvider : ISmsProvider
    {
        private string _username { get; set; }
        private string _password { get; set; }
        private string _title { get; set; }


        public NetGsmProvider(string username, string password,string title)
        {
             if (username == null)
                throw new ArgumentException("Username null error");
            if (password == null)
                throw new ArgumentException("password null error");
            if (title == null)
                throw new ArgumentException("title null error");

            this._username = username;
            this._password = password;
            this._title = title;

        }


        public async Task SendAsync(string phonenumber, string messagecontent)
        {
       
            if (phonenumber.Length != 11)
                throw new ArgumentException("Telefon numarası uyumlu değil : " + phonenumber);

            string dt = "";
            dt += "<?xml version='1.0' encoding='UTF-8'?>";
            dt += "<mainbody>";
            dt += "<header>";
            dt += "<company dil='TR'>Netgsm</company>";
            dt += $"<usercode>{_username}</usercode>";
            dt += $"<password>{_password}</password>";
            dt += "<type>1:n</type>";
            dt += $"<msgheader>{_title}</msgheader>";
            dt += "</header>";
            dt += "<body>";
            dt += "<msg>";
            dt += $"<![CDATA[{messagecontent}]]>";
            dt += "</msg>";
            dt += $"<no>{phonenumber}</no>";
            dt += "</body>  ";
            dt += "</mainbody>";


            string getData = await SmsHelper.Post("https://api.netgsm.com.tr/sms/send/xml", dt);
            if (getData == "-1")
                throw new ArgumentException("Servis Hatası");

            string getCode = getData.Split(' ')[0].ToString();


            if (getCode == "00" || getCode == "01" || getCode == "02")
            {
                string Id = getData.Split(' ')[1].ToString();
            }
            else if (getCode == "20")
                throw new ArgumentException("Mesaj metninde ki problemden dolayı gönderilemediğini veya standart maksimum mesaj karakter sayısını geçti.");
            else if (getCode == "30")
                throw new ArgumentException("Geçersiz kullanıcı adı , şifre veya kullanıcınızın API erişim iznininiz bulunmamakta.");
            else if (getCode == "40")
                throw new ArgumentException("Mesaj başlığınızın (gönderici adınızın) sistemde tanımlı değil. ");
            else if (getCode == "70")
                throw new ArgumentException("Hatalı sorgulama. Gönderdiğiniz parametrelerden birisi hatalı veya zorunlu alanlardan birinin eksik. ");
            else
                throw new ArgumentException("Bilinmeyen bir hata oluştu");


        }

        public async Task<double> GetCreditAsync()
        {
            string getData = new WebClient().DownloadString($"https://api.netgsm.com.tr/balance/list/get/?usercode={_username}&password={_password}");

            if (getData.Split(' ')[0].ToString() == "00")
                return double.Parse(getData.Split(' ')[1].ToString().Replace(',', '.'));
            else return 0; 

        }

    }
}
