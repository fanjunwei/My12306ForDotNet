using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Windows.Forms;
using System.Drawing;

namespace My12306
{
    delegate void void__string(string text);
    delegate void void__control(Control c);
    delegate void addControlHandler(List<KeyValue> keyvalue);
    delegate void setImageHandler(Bitmap  map);
    delegate void setDataSourceHandler(object data);
    class Helper
    {
        static CookieContainer _cookies = new CookieContainer();

        public static CookieContainer Cookies
        {
            get { return _cookies; }
            set { _cookies = value; }
        }
        
        public static bool AcceptAllCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        
    }
}
