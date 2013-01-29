using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace My12306
{
    public partial class Form1 : Form
    {
        List<KeyValue> defaultKeyValue = new List<KeyValue>();
        public Form1()
        {
            InitializeComponent();
        }

        private void setText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new void__string(setText), text);
            }
            else
            {
                richTextBox1.Text = text;
            }
        }

        private void addInput(Control c)
        {
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new void__control(addInput), c);
            }
            else
            {
                panel1.Controls.Add(c);
            }
        }
        private void getLoginImgCode()
        {
            getImage("https://dynamic.12306.cn/otsweb/passCodeAction.do?rand=sjrand");
        }
        private void initLogin()
        {
            //JSESSIONID=114FBDF26F5C2B07D29DB2EC36623E35";document.cookie="BIGipServerotsweb=2463367434.62495.0000
            //2178154762.48160.0000";document.cookie="BIGipServerportal=3084124426.17183.0000";self.location.reload();
            //HttpGet("https://dynamic.12306.cn/otsweb/");
            HtmlForm form = HttpGet("https://dynamic.12306.cn/otsweb/loginAction.do?method=init");
            setDefalutTag(form);
            getLoginImgCode();

            //Helper.Cookies.Add(new Uri("https://dynamic.12306.cn"), new Cookie("JSESSIONID", "E823324EF523FDF97088F148EBE5E747"));
            //Helper.Cookies.Add(new Uri("https://dynamic.12306.cn"), new Cookie("BIGipServerotsweb", "2463367434.62495.0000"));

            //HttpGet("https://dynamic.12306.cn/otsweb/order/querySingleAction.do?method=init");
            //HttpGet("https://dynamic.12306.cn/otsweb/main.jsp");
            //
        }
        private void setQueryDataSource(object data)
        {
            if (dgvQuery.InvokeRequired)
            {
                dgvQuery.Invoke(new setDataSourceHandler(setQueryDataSource), data);
            }
            else
            {
                dgvQuery.DataSource = data;
            }
        }
        private void query()
        {
            //JSESSIONID=114FBDF26F5C2B07D29DB2EC36623E35";document.cookie="BIGipServerotsweb=2463367434.62495.0000
            //2178154762.48160.0000";document.cookie="BIGipServerportal=3084124426.17183.0000";self.location.reload();
            //HttpGet("https://dynamic.12306.cn/otsweb/");
            string search = getText(txtQueryString.Text);
            search = search.Replace("\\n", "\r\n");
            search = search.Replace("&nbsp;", "");
            StringReader reader = new StringReader(search);
            string line;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(string));
            table.Columns.Add("车次", typeof(string));
            table.Columns.Add("发站", typeof(string));
            table.Columns.Add("到站", typeof(string));
            table.Columns.Add("历时", typeof(string));
            table.Columns.Add("商务座", typeof(string));
            table.Columns.Add("特等座", typeof(string));
            table.Columns.Add("一等座", typeof(string));
            table.Columns.Add("二等座", typeof(string));
            table.Columns.Add("高级软卧", typeof(string));
            table.Columns.Add("软卧", typeof(string));
            table.Columns.Add("硬卧", typeof(string));
            table.Columns.Add("软座", typeof(string));
            table.Columns.Add("硬座", typeof(string));
            table.Columns.Add("无座", typeof(string));
            table.Columns.Add("其他", typeof(string));
            table.Columns.Add("购票", typeof(string));
            Regex reg = new Regex("<.*?>", RegexOptions.Singleline);
            while ((line = reader.ReadLine()) != null)
            {
                DataRow row = table.NewRow();
                string[] strs = line.Split(',');
                bool find = false;
                for (int i = 0; i < strs.Length; i++)
                {
                    string item = strs[i];
                    Regex commit = new Regex("getSelected\\('(.*?)'\\)", RegexOptions.Singleline);
                    Match mc = commit.Match(item);
                    string commitstr = null;
                    if (mc.Success)
                    {
                        commitstr = mc.Groups[1].Value;
                    }
                    item = reg.Replace(item, "");
                    if (i == 1 && item == txtTrainCode.Text)
                    {
                        find = true;
                    }
                    if (find && commitstr != null)
                    {
                        yuding(commitstr);
                    }
                    row[i] = item;
                }
                table.Rows.Add(row);

            }
            table.AcceptChanges();
            setQueryDataSource(table);

            search = reg.Replace(search, "");
            //setText(search);

            //
        }
        private void yuding(string commitstr)
        {
            string yuanshi =
@"station_train_code#K558
train_date#2013-02-06
seattype_num#
from_station_telecode#ZZF
to_station_telecode#SHH
include_student#00
from_station_telecode_name#郑州
to_station_telecode_name#上海
round_train_date#2013-02-07
round_start_time_str#00:00--24:00
single_round_type#1
train_pass_type#QB
train_class_arr#QB#D#Z#T#K#QT#
start_time_str#00:00--24:00
lishi#13:15
train_start_time#00:30
trainno4#4a0000K5590D
arrive_time#13:45
from_station_name#郑州
to_station_name#上海
from_station_no#07
to_station_no#18
ypInfoDetail#1*****30024*****00001*****00003*****0002
mmStr#B1F8667617E02CC7B0712C6A22C1DB50DC6242E7103476AD921D8959
locationCode#Y1";

            HtmlForm form = new HtmlForm(new Uri("https://dynamic.12306.cn/otsweb/order/querySingleAction.do?method=submutOrderRequest"));
            form.Referer = "https://dynamic.12306.cn/otsweb/order/querySingleAction.do?method=init";
            StringReader reader = new StringReader(yuanshi);
            string line = null;

            while ((line = reader.ReadLine()) != null)
            {
                string[] tags = line.Split('#');
                form.setTagValue(tags[0], tags[1]);
            }
            //form.setTagValue("station_train_code", "K558");
            //form.setTagValue("train_date", "2013-02-06");
            //form.setTagValue("seattype_num", "");
            //form.setTagValue("from_station_telecode", "ZZF");
            //form.setTagValue("to_station_telecode", "SHH");
            //form.setTagValue("include_student", "00");
            //form.setTagValue("from_station_telecode_name", "郑州");
            //form.setTagValue("to_station_telecode_name", "上海");
            //form.setTagValue("round_train_date", "2013-02-07");
            //form.setTagValue("round_start_time_str", "00:00--24:00");
            //form.setTagValue("single_round_type", "1");


            //form.setTagValue("train_pass_type", "QB");
            //form.setTagValue("train_class_arr", "QB#D#Z#T#K#QT#");
            //form.setTagValue("start_time_str", "00:00--24:00");
            //form.setTagValue("lishi", "13:15");
            //form.setTagValue("train_start_time", "00:30");
            //form.setTagValue("from_station_name", "上海");
            //form.setTagValue("from_station_name", "上海");
            //form.setTagValue("from_station_name", "上海");
            //form.setTagValue("to_station_no", "18");
            //form.setTagValue("station_train_code", "K558");



            reader.Close();
            string[] commsp = commitstr.Split('#');
            form.setTagValue("station_train_code", commsp[0]);
            form.setTagValue("lishi", commsp[1]);
            form.setTagValue("train_start_time", commsp[2]);
            form.setTagValue("trainno4", commsp[3]);
            form.setTagValue("from_station_telecode", commsp[4]);
            form.setTagValue("to_station_telecode", commsp[5]);
            form.setTagValue("arrive_time", commsp[6]);
            form.setTagValue("from_station_telecode_name", commsp[7]);
            form.setTagValue("to_station_telecode_name", commsp[8]);
            form.setTagValue("from_station_no", commsp[9]);
            form.setTagValue("to_station_no", commsp[10]);
            form.setTagValue("ypInfoDetail", commsp[11]);
            form.setTagValue("mmStr", commsp[12]);
            form.setTagValue("locationCode", commsp[13]);

            string res= form.post();
            res = getText("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=init");
            //res = getText("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=init");
            setText(res);
        }
        private void setImage(Bitmap map)
        {
            if (picCode.InvokeRequired)
            {
                picCode.Invoke(new setImageHandler(setImage), map);
            }
            else
            {
                picCode.Image = map;
            }
        }
        private void getImage(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(uri);
                http.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
                //http.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/4.0");
                http.CookieContainer = Helper.Cookies;
                http.Referer = url;
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                foreach (Cookie c in response.Cookies)
                {
                    Helper.Cookies.Add(c);
                }
                Stream stream = response.GetResponseStream();
                Bitmap map = new Bitmap(stream);
                setImage(map);

            }
            catch 
            {

                Console.WriteLine("连接失败");

            }
        }
        ///otsweb/loginAction.do?method=loginAysnSuggest
        private string getText(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(uri);
            http.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
            //http.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/4.0");
            http.CookieContainer = Helper.Cookies;
            http.Referer = url;
            HttpWebResponse response = (HttpWebResponse)http.GetResponse();
            foreach (Cookie c in response.Cookies)
            {
                Helper.Cookies.Add(c);
            }
            Stream stream = response.GetResponseStream();

            StringBuilder sb = new StringBuilder();

            StreamReader reader = new StreamReader(stream);
            string res = reader.ReadToEnd();
            setText(res);
            return res;

        }
        private HtmlForm HttpGet(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(uri);
                http.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
                //http.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/4.0");
                http.CookieContainer = Helper.Cookies;
                http.Referer = url;
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                foreach (Cookie c in response.Cookies)
                {
                    Helper.Cookies.Add(c);
                }
                Stream stream = response.GetResponseStream();

                StringBuilder sb = new StringBuilder();

                StreamReader reader = new StreamReader(stream);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(line + "\r\n");
                }
                reader.Close();
                setText(sb.ToString());
                HtmlForm form = new HtmlForm(uri, sb.ToString());

                addControl(form.Inputs);

                //string tags = "";
                //foreach (KeyValue str in form.Inputs)
                //{
                //    tags += str + "\n";
                //}
                return form;

            }

            catch (UriFormatException ex)
            {

                Console.WriteLine("无效的URL");

            }

            catch (IOException ex)
            {

                Console.WriteLine("连接失败");

            }
            return null;
        }





        private void doKeyValue(List<KeyValue> keyvalue)
        {

        }
        private void addControl(List<KeyValue> keyvalue)
        {
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(new addControlHandler(addControl), keyvalue);
            }
            else
            {

                panel1.Controls.Clear();
                int top = 0;
                foreach (KeyValue v in keyvalue)
                {
                    if (v.TagType == "password" || v.TagType == "text" || v.TagType == "checkbox")
                    {
                        MyInput input = new MyInput();

                        input.IsCheckbox = v.TagType == "checkbox";
                        input.tagName = v.Key;
                        input.CreateControl();
                        v.inputControl = input;
                        input.Top = top;
                        top += input.Height;
                        addInput(input);

                        if (v.Key == "randCode")
                        {
                            input.txtValue.TextChanged += new EventHandler(loginRand_TextChanged);
                            input.txtValue.Tag = v.getForm();
                        }
                    }
                }
                if (keyvalue != null && keyvalue.Count > 0)
                {
                    Button btnLogin = new Button();
                    btnLogin.Text = "提交";
                    btnLogin.Tag = keyvalue[0].getForm();
                    btnLogin.Click += new EventHandler(btnLogin_Click);
                    btnLogin.Top = top;
                    addInput(btnLogin);
                    this.AcceptButton = btnLogin;
                }
            }
        }

        void loginRand_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;
            if (txtbox.Text.Length == 4)
            {
                string res = getText("https://dynamic.12306.cn/otsweb/loginAction.do?method=loginAysnSuggest");
                JObject o = (JObject)JsonConvert.DeserializeObject(res);

                HtmlForm form = (HtmlForm)txtbox.Tag;
                if (o["randError"].ToString() == "Y")
                {
                    form.setTagValue("loginRand", o["loginRand"].ToString());
                }
                string postres = form.post();
                bool error = false;
                string errormsg = "";
                if (postres.IndexOf("请输入正确的验证码") != -1)
                {
                    errormsg = "验证错误";
                    getLoginImgCode();
                    txtbox.Clear();
                    txtbox.Focus();
                    error = true;
                }
                else if (postres.IndexOf("密码输入错误") != -1)
                {
                    Regex reg = new Regex("\"(密码输入错误.*?)\"", RegexOptions.Singleline);
                    Match m = reg.Match(postres);
                    if (m.Success)
                    {
                        errormsg = m.Groups[1].Value;
                    }
                    else
                    {
                        errormsg = "验证错误";
                    }

                    getLoginImgCode();
                    txtbox.Clear();
                    txtbox.Focus();
                    error = true;
                }
                if (postres.IndexOf("我的订单") != -1)
                {
                    Thread t = new Thread(new ThreadStart(query));
                    t.Start();
                }
                else
                {
                    if (error)
                    {
                        setText(errormsg);
                    }
                    else
                    {
                        setText(postres);
                    }
                    txtbox.Focus();
                    txtbox.SelectAll();
                }
            }
        }

        void btnLogin_Click(object sender, EventArgs e)
        {
            string res = getText("https://dynamic.12306.cn/otsweb/loginAction.do?method=loginAysnSuggest");
            JObject o = (JObject)JsonConvert.DeserializeObject(res);
            Button btn = (Button)sender;
            HtmlForm form = (HtmlForm)btn.Tag;
            if (o["randError"].ToString() == "Y")
            {
                form.setTagValue("loginRand", o["loginRand"].ToString());
            }
            string postres = form.post();
            bool error = false;
            string errormsg = "";
            if (postres.IndexOf("请输入正确的验证码") != -1)
            {
                errormsg = "验证错误";
                getLoginImgCode();
                error = true;
            }
            else if (postres.IndexOf("密码输入错误") != -1)
            {
                Regex reg = new Regex("\"(密码输入错误.*?)\"", RegexOptions.Singleline);
                Match m = reg.Match(postres);
                if (m.Success)
                {
                    errormsg = m.Groups[1].Value;
                }
                else
                {
                    errormsg = "验证错误";
                }

                getLoginImgCode();

                error = true;
            }
            if (postres.IndexOf("我的订单") != -1)
            {
                Thread t = new Thread(new ThreadStart(query));
                t.Start();
            }
            else
            {
                if (error)
                {
                    setText(errormsg);
                }
                else
                {
                    setText(postres);
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            defaultKeyValue.Add(new KeyValue("loginUser.user_name", "fanjunwei"));
            defaultKeyValue.Add(new KeyValue("user.password", "aco00o68b123"));
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(Helper.AcceptAllCertificate);
            Thread t = new Thread(new ThreadStart(initLogin));
            t.Start();
        }

        public void setDefalutTag(HtmlForm form)
        {
            foreach (KeyValue kv in defaultKeyValue)
            {
                form.setTagValue(kv.Key, kv.Value);
            }
        }

        private void picCode_Click(object sender, EventArgs e)
        {
            getLoginImgCode();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(query));
            t.Start();
        }



    }

}
