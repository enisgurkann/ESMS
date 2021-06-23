using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EB2B.SMS.Helper
{
    public static class SmsHelper
    {
        public static async Task<string> Post(string PostAddress, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                var uri = new Uri(PostAddress);
                HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = await wUpload.UploadDataTaskAsync(uri, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }
        public static string ClearPhoneNumberText(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return null;
            phoneNumber = Regex.Replace(phoneNumber, @"^(\+)|\D", "$1")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("-", "")
            .Replace("+", "")
            .Replace("(", "")
            .Replace("(", "")
            .Trim();

            if (phoneNumber.Length > 11 && phoneNumber.Substring(0, 2) == "90")
                phoneNumber = phoneNumber.Remove(0, 1);

            if (phoneNumber.Length == 10)
                phoneNumber = "0" + phoneNumber;

            return phoneNumber;
        }

        public static async Task<string> Get(string Adress)
        {
            try
            {
                WebClient wUpload = new WebClient();
                var uri = new Uri(Adress);
                HttpWebRequest request = WebRequest.Create(Adress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes("");
                Byte[] bResponse = await wUpload.UploadDataTaskAsync(uri, "GET", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }
    }
}
