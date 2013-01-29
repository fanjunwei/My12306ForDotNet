using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace My12306
{
    public class HtmlForm
    {
        public HtmlForm(Uri actionUri)
        {
            this._actionUri = actionUri;
            _inputs = new List<KeyValue>();

        }
        public HtmlForm(Uri baseUri, string html)
        {
            this._baseUri = baseUri;
            this._inputs = getKeyValue(getInputTag(html));
            getFormTag(html);
        }
        private Uri _baseUri;

        private Uri _actionUri;
        private List<KeyValue> _inputs;
        public void setTagValue(string key, string value)
        {

            foreach (KeyValue kv in _inputs)
            {
                if (kv.Key == key)
                {
                    kv.Value = value;
                    break;
                }
            }

            _inputs.Add(new KeyValue(this, key, value, "hiden"));
        }
        private string _referer;

        public string Referer
        {
            get { return _referer; }
            set { _referer = value; }
        }

        public List<KeyValue> Inputs
        {
            get { return _inputs; }
        }
        private void getFormTag(string html)
        {
            List<string> res = new List<string>();
            Regex re = new Regex("<form .*?>", RegexOptions.Singleline);
            html = re.Match(html).Value;
            Regex actionReg = new Regex("action\\s*=\\s*\"(.*?)\"", RegexOptions.Singleline);
            Match mAction = actionReg.Match(html);
            string actionUrl = mAction.Groups[1].Value;
            _actionUri = new Uri(_baseUri, actionUrl);


        }
        private List<string> getInputTag(string html)
        {
            List<string> res = new List<string>();
            Regex re = new Regex("<input .*?>", RegexOptions.Singleline);
            MatchCollection matchs = re.Matches(html);
            foreach (Match m in matchs)
            {
                res.Add(m.Value);
            }

            return res;
        }
        private List<KeyValue> getKeyValue(List<string> tags)
        {
            List<KeyValue> res = new List<KeyValue>();
            foreach (string str in tags)
            {
                Regex keyReg = new Regex("name\\s*=\\s*\"(.*?)\"", RegexOptions.Singleline);
                Regex valueReg = new Regex("value\\s*=\\s*\"(.*?)\"", RegexOptions.Singleline);
                Regex typeReg = new Regex("type\\s*=\\s*\"(.*?)\"", RegexOptions.Singleline);
                Match mKey = keyReg.Match(str);
                Match mValue = valueReg.Match(str);
                Match mType = typeReg.Match(str);
                string strKey = null;
                string strValue = "";
                string strType = "";
                if (mKey.Success)
                {
                    strKey = mKey.Groups[1].Value;
                }
                if (mValue.Success)
                {
                    strValue = mValue.Groups[1].Value;
                }
                if (mType.Success)
                {
                    strType = mType.Groups[1].Value;
                }
                if (strKey != null)
                {
                    res.Add(new KeyValue(this, strKey, strValue, strType));
                }

            }
            return res;
        }

        public string post()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValue kv in _inputs)
            {
                string value = kv.ToString();
                if (value != null)
                {
                    sb.Append(value + "&");
                }
            }
            string poststr = sb.ToString();
            poststr = poststr.TrimEnd('&');
            byte[] bytestopost = Encoding.UTF8.GetBytes(poststr);
            Uri uri = _actionUri;
            HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(uri);
            http.Method = "post";
            if (_referer != null)
            {
                http.Referer = _referer;

            }
            else
                http.Referer = uri.AbsoluteUri;
            http.ContentType = "application/x-www-form-urlencoded";
            http.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
            //http.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/4.0");
            http.CookieContainer = Helper.Cookies;
            //http.ContentLength=bytestopost.Length;
            Stream postStream = http.GetRequestStream();
            postStream.Write(bytestopost, 0, bytestopost.Length);
            postStream.Flush();
            postStream.Close();
            HttpWebResponse response = (HttpWebResponse)http.GetResponse();
            foreach (Cookie c in response.Cookies)
            {
                Helper.Cookies.Add(c);
            }

            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();
            return str;
        }

    }
}
