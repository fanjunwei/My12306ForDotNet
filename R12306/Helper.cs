using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;

namespace R12306
{
    delegate void void__void();
    delegate void void__TextBox_string(TextBox t, string text);
    delegate void void__ComboBox_string(ComboBox t, string text);
    delegate void void__Lable_string(Label t, string text);
    delegate void void_string(string text);
    delegate void void_string_bool(string text,bool show);
    delegate void void__control(Control c);
    delegate void addControlHandler(List<KeyValue> keyvalue);
    delegate void setImageHandler(PictureBox pcx, Bitmap map);
    delegate void setDataSourceHandler(object data);
    class Helper
    {
        public static int Timeout = 5000;
       
        public static string UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";

       

        public static bool AcceptAllCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string A_0, string A_1, string A_2);
        public static void OpenIE()
        {
            List<Cookie> cookies = GetAllCookies(CookiesManage.CurrCookies);
            foreach (Cookie cookie in cookies)
            {
             Helper.InternetSetCookie(
                    "https://" + cookie.Domain.ToString(),
                    cookie.Name.ToString(),
                    cookie.Value.ToString() + ";expires=Sun,22-Feb-2099 00:00:00 GMT");
            }
            Process.Start("IExplore.exe", "https://dynamic.12306.cn/otsweb/");
        }

        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }

    }
}
