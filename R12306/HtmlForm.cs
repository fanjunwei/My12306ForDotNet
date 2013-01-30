using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace R12306
{
    public delegate void PostCallBackHandler(HtmlForm sender, string result);
    public class HtmlForm
    {
        public event PostCallBackHandler PostCallBack;
        public HtmlForm(string actionUri)
        {

            this._actionUri = new Uri(actionUri);

        }
        private Uri _actionUri;

        private List<KeyValue> _queryString = new List<KeyValue>();

        public List<KeyValue> QueryString
        {
            get { return _queryString; }
        }


        public Uri ActionUri
        {
            get { return _actionUri; }
            set { _actionUri = value; }
        }
        private List<KeyValue> _inputs = new List<KeyValue>();
        public void setTagValue(string key, string value)
        {

            foreach (KeyValue kv in _inputs)
            {
                if (kv.Key == key)
                {
                    kv.Value = value;
                    return;
                }
            }

            _inputs.Add(new KeyValue(this, key, value, "hiden"));
        }

        public string getTagValue(string key)
        {

            foreach (KeyValue kv in _inputs)
            {
                if (kv.Key == key)
                {
                    return kv.Value;

                }
            }
            return null;

        }
        public void AddTag(string key, string value)
        {
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
        //private List<KeyValue> getTags(List<string> tags)
        //{
        //    List<KeyValue> res = new List<KeyValue>();
        //    foreach (string str in tags)
        //    {
        //        Regex keyReg = new Regex("name\\s*=\\s*\"(.*?)\"", RegexOptions.Singleline);
        //        Regex valueReg = new Regex("value\\s*=\\s*\"(.*?)\"", RegexOptions.Singleline);
        //        Regex typeReg = new Regex("type\\s*=\\s*\"(.*?)\"", RegexOptions.Singleline);
        //        Match mKey = keyReg.Match(str);
        //        Match mValue = valueReg.Match(str);
        //        Match mType = typeReg.Match(str);
        //        string strKey = null;
        //        string strValue = "";
        //        string strType = "";
        //        if (mKey.Success)
        //        {
        //            strKey = mKey.Groups[1].Value;
        //        }
        //        if (mValue.Success)
        //        {
        //            strValue = mValue.Groups[1].Value;
        //        }
        //        if (mType.Success)
        //        {
        //            strType = mType.Groups[1].Value;
        //        }
        //        if (strKey != null)
        //        {
        //            res.Add(new KeyValue(this, strKey, strValue, strType));
        //        }

        //    }
        //    return res;
        //}

        string res;

        public void post()
        {
            Thread t = new Thread(new ThreadStart(run));
            t.Start();
        }
        public void ClearTag()
        {
            _inputs.Clear();
        }
        private void run()
        {
            try
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
                Uri uri;
                if (_queryString.Count > 0)
                {
                    string temurl = ActionUri.AbsoluteUri;
                    if (temurl.IndexOf("?") == -1)
                    {
                        temurl += "?";
                    }
                    else
                    {
                        temurl += "&";
                    }
                    foreach (KeyValue kv in _queryString)
                    {
                        temurl += kv.ToString() + "&";
                    }
                    temurl.TrimEnd('&');
                    uri = new Uri(temurl);
                }
                else
                {
                    uri = ActionUri;
                }

                HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(uri);
                http.Method = "POST";
                if (_referer != null)
                {
                    http.Referer = _referer;

                }
                else
                    http.Referer = uri.AbsoluteUri;
                http.Accept = "application/json, text/javascript, */*";
                http.ContentType = "application/x-www-form-urlencoded";
                http.UserAgent = Helper.UserAgent;
                http.CookieContainer = CookiesManage.CurrCookies;
                http.Timeout = Helper.Timeout;
                Stream postStream = http.GetRequestStream();
                postStream.Write(bytestopost, 0, bytestopost.Length);
                postStream.Flush();
                postStream.Close();
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                foreach (Cookie c in response.Cookies)
                {
                    CookiesManage.CurrCookies.Add(c);
                }

                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                res = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                if (PostCallBack != null)
                {
                    PostCallBack(this, res);
                }
            }
            catch (WebException)
            {
                
                Thread.Sleep(500);
                post();
            }

        }
        public string debug()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValue kv in _inputs)
            {
                sb.Append(kv.Key + ":" + kv.Value + "\n");
                //System.Diagnostics.Debug.WriteLine(kv.Key + ":" + kv.Value);
            }
            return sb.ToString();
        }


    }
}
