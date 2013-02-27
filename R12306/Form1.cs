using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Security;
using System.Collections;
using System.Media;

namespace R12306
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TimeSpan commitDelay = new TimeSpan(0, 0, 5);
        TimeSpan checkDelay = new TimeSpan(0, 0, 1);
        List<passengerTicketItem> selectedPassengers = new List<passengerTicketItem>();
        List<passengerTicketItem> allPassengers = new List<passengerTicketItem>();
        HtmlForm loginForm = new HtmlForm("https://dynamic.12306.cn/otsweb/loginAction.do?method=login");
        HtmlForm yudingForm = new HtmlForm("https://dynamic.12306.cn/otsweb/order/querySingleAction.do?method=submutOrderRequest");
        HtmlForm commitForm = new HtmlForm("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=checkOrderInfo");
        HtmlForm checkForm = new HtmlForm("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=confirmSingleForQueue");
        List<CbxItem> stations = new List<CbxItem>();
        TrainInfo currTrainInfo = null;
        bool isLogin = false;
        private string getText(string url, bool isPost)
        {
            return getText(url, isPost, CookiesManage.CurrCookies);
        }
        private string getText(string url, bool isPost, CookieContainer useCookie)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(uri);
                http.UserAgent = Helper.UserAgent;

                http.CookieContainer = useCookie;
                http.Headers.Add("X-Requested-With", "XMLHttpRequest");
                http.Timeout = Helper.Timeout;
                if (isPost)
                {
                    http.Method = "POST";
                    http.ContentLength = 0;
                }
                else
                {
                    http.Method = "GET";
                }
                http.Referer = url;
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                foreach (Cookie c in response.Cookies)
                {
                    useCookie.Add(c);
                }
                using (Stream stream = response.GetResponseStream())
                {

                    StringBuilder sb = new StringBuilder();

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string res = reader.ReadToEnd();

                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                addLog("网络错误:" + ex.Message, true);
                return null;
            }

        }
        private void getLoginImgCode()
        {
            Bitmap map = null;
            while (map == null)
            {
                map = getImage("https://dynamic.12306.cn/otsweb/passCodeAction.do?rand=sjrand", "https://dynamic.12306.cn/otsweb/loginAction.do?method=init");
                if (map == null)
                {
                    addLog("获取验证码错误", true);
                    Thread.Sleep(500);
                }
            }
            setImage(pcxLoginImgCode, map);
            if (txtPassword.Text != "" && txtUserName.Text != "")
            {
                setTextAndFocus(txtLoginImgCode, "");
            }
        }
        private void getCommitImgCode()
        {
            Bitmap map = null;
            Random r = new Random((int)DateTime.Now.Ticks);
            string url = "https://dynamic.12306.cn/otsweb/passCodeAction.do?rand=randp&" + r.NextDouble().ToString();
            while (map == null)
            {
                map = getImage(url, "https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=init");
                if (map == null)
                {
                    addLog("获取验证码错误", true);
                    Thread.Sleep(500);
                }
            }
            setImage(pcxCommitCode, map);
            setTextAndFocus(txtCommitCode, "");
        }
        private void setImage(PictureBox pcx, Bitmap map)
        {
            if (pcx.InvokeRequired)
            {
                pcx.Invoke(new setImageHandler(setImage), pcx, map);
            }
            else
            {
                pcx.Image = map;
            }
        }
        private Bitmap getImage(string url, string referer)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(uri);
                http.UserAgent = Helper.UserAgent;

                http.CookieContainer = CookiesManage.CurrCookies;
                http.Referer = referer;
                http.Timeout = Helper.Timeout;
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                foreach (Cookie c in response.Cookies)
                {
                    CookiesManage.CurrCookies.Add(c);
                }
                using (Stream stream = response.GetResponseStream())
                {
                    Bitmap map = new Bitmap(stream);
                    return map;
                }

            }

            catch (Exception ex)
            {
                addLog("网络错误：" + ex.Message, true);
                return null;
            }


        }
        void setYuanshi(HtmlForm form, string yuanshi)
        {
            using (StringReader reader = new StringReader(yuanshi))
            {
                string line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] tags = line.Split('#');
                    form.setTagValue(tags[0], tags[1]);
                }
            }

        }
        void login()
        {
            Thread t = new Thread(new ThreadStart(runLogin));
            t.Start();
        }
        void runLogin()
        {
            addLog("开始登陆", true);
            Helper.Timeout = 5000;
            string yuanshi =
@"loginRand#
loginUser.user_name#
nameErrorFocus#
user.password#
passwordErrorFocus#
randCode#";
            setYuanshi(loginForm, yuanshi);
            while (true)
            {
                string res = getText("https://dynamic.12306.cn/otsweb/loginAction.do?method=loginAysnSuggest", false);
                if (res != null)
                {
                    try
                    {
                        JObject o = (JObject)JsonConvert.DeserializeObject(res);
                        if (o["randError"].ToString() == "Y")
                        {
                            loginForm.setTagValue("loginRand", o["loginRand"].ToString());
                            break;
                        }


                    }
                    catch
                    {

                    }
                }

                addLog("获取TOKEN错误，重新获取", true);

                Thread.Sleep(1000);
            }

            loginForm.setTagValue("loginUser.user_name", txtUserName.Text);
            loginForm.setTagValue("user.password", txtPassword.Text);
            loginForm.setTagValue("randCode", txtLoginImgCode.Text);
            loginForm.post();


        }
        void loginForm_PostCallBack(HtmlForm sender, string result)
        {
            bool error = false;
            string errormsg = "";
            if (result.IndexOf("请输入正确的验证码") != -1)
            {
                errormsg = "验证错误";

                getLoginImgCode();

                error = true;
            }
            else if (result.IndexOf("密码输入错误") != -1)
            {
                Regex reg = new Regex("\"(密码输入错误.*?)\"", RegexOptions.Singleline);
                Match m = reg.Match(result);
                if (m.Success)
                {
                    errormsg = m.Groups[1].Value;
                }
                else
                {
                    errormsg = "密码输入错误";
                }
                setTextAndFocus(txtPassword, "");
                error = true;
            }
            else if (result.IndexOf("您的用户已经被锁定") != -1)
            {
                Regex reg = new Regex("\"(您的用户已经被锁定.*?)\"", RegexOptions.Singleline);
                Match m = reg.Match(result);
                if (m.Success)
                {
                    errormsg = m.Groups[1].Value;
                }
                else
                {
                    errormsg = "您的用户已经被锁定";
                }


                setTextAndFocus(txtPassword, "");
                error = true;
            }
            else if (result.IndexOf("登录名不存在") != -1)
            {
                setText(txtPassword, "");
                setTextAndFocus(txtUserName, "");
                errormsg = "登录名不存在";
                error = true;
            }
            //登录名不存在
            if (result.IndexOf("我的订单") != -1)
            {
                addLog("登陆成功", true);
                isLogin = true;
                Regex re = new Regex("u_name = '(.*?)'", RegexOptions.Singleline);
                Match m = re.Match(result);
                if (m.Success)
                {
                    setLable(lblLoginError, "【" + m.Groups[1].Value + "】已登录");
                }
                else
                {
                    setLable(lblLoginError, "登录成功");
                }
                Config config = Config.getConfig();
                config.Password = txtPassword.Text;
                config.UserName = txtUserName.Text;
                Config.SaveInfo();
                getPassenger();
            }
            else
            {
                if (error)
                {
                    addLog(errormsg, true);
                    setLable(lblLoginError, errormsg);
                }
                else
                {
                    Thread.Sleep(2000);
                    login();
                }
            }
        }
        private void setLable(Label lable, string text)
        {
            if (lable.InvokeRequired)
            {
                lable.Invoke(new void__Lable_string(setLable), lable, text);
            }
            else
            {
                lable.Text = text;
            }
        }
        private void setText(TextBox textbox, string text)
        {
            if (textbox.InvokeRequired)
            {
                textbox.Invoke(new void__TextBox_string(setText), textbox, text);
            }
            else
            {
                textbox.Text = text;
            }
        }
        private void setTextAndFocus(TextBox textbox, string text)
        {
            if (textbox.InvokeRequired)
            {
                textbox.Invoke(new void__TextBox_string(setTextAndFocus), textbox, text);
            }
            else
            {
                textbox.Text = text;
                textbox.Focus();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }
        private void initSeat()
        {
            cbxSeat.Items.Add(new CbxItem("二等座", "O"));
            cbxSeat.Items.Add(new CbxItem("一等座", "M"));
            cbxSeat.Items.Add(new CbxItem("商务座", "9"));
            cbxSeat.Items.Add(new CbxItem("特等座", "P"));
            cbxSeat.Items.Add(new CbxItem("高级软卧", "6"));
            cbxSeat.Items.Add(new CbxItem("软卧", "4"));
            cbxSeat.Items.Add(new CbxItem("硬卧", "3"));
            cbxSeat.Items.Add(new CbxItem("软座", "2"));
            cbxSeat.Items.Add(new CbxItem("硬座", "1"));
            cbxSeat.Items.Add(new CbxItem("无座", "empty"));
            cbxSeat.SelectedIndex = Config.getConfig().SeatIndex;
            TrainInfo.Seat = ((CbxItem)cbxSeat.Items[cbxSeat.SelectedIndex]).code;

        }
        private void setCbxText(ComboBox cbx, string text)
        {
            if (cbx.InvokeRequired)
            {
                cbx.Invoke(new void__ComboBox_string(setCbxText), cbx, text);
            }
            else
            {
                cbx.Text = text;
            }
        }
        private void AddPassengerToList()
        {
            Config config = Config.getConfig();
            if (cbxListPassenger.InvokeRequired)
            {
                cbxListPassenger.Invoke(new void__void(AddPassengerToList));
            }
            else
            {
                cbxListPassenger.Items.Clear();
                foreach (passengerTicketItem item in allPassengers)
                {
                    if (config.SelectPassengers.Contains(item.Name))
                    {
                        cbxListPassenger.Items.Add(item, true);
                    }
                    else
                    {
                        cbxListPassenger.Items.Add(item, false);
                    }
                }
                addLog("联系人加载完成。", true);
            }

        }
        private void getPassenger()
        {
            addLog("初始化常用联系人...", true);
            string tem1 = null;
            JObject json = null;
            while (true)
            {
                tem1 = getText("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=getpassengerJson", true);
                if (tem1 == null)
                {
                    addLog("初始化常用联系人错误，稍候重试", true);
                    Thread.Sleep(500);
                    continue;
                }
                try
                {
                    json = (JObject)JsonConvert.DeserializeObject(tem1);
                }
                catch
                {
                    addLog("初始化常用联系人错误，稍候重试", true);
                    Thread.Sleep(500);
                    continue;
                }
                break;
            }
            addLog(tem1, false);
            JArray jsonPassengersArray = (JArray)json["passengerJson"];
            allPassengers.Clear();
            foreach (JObject p in jsonPassengersArray)
            {
                passengerTicketItem item = new passengerTicketItem(0);
                item.Cardno = p["passenger_id_no"].ToString();
                item.Cardtype = p["passenger_id_type_code"].ToString();
                item.Mobileno = p["mobile_no"].ToString();
                item.Name = p["passenger_name"].ToString();
                item.Ticket = p["passenger_type"].ToString();
                allPassengers.Add(item);

            }
            AddPassengerToList();

        }
        private void init()
        {
            Config config = Config.getConfig();
            addLog("初始化...", true);
            getStations();

            if (config.FromStationName != null)
            {
                setCbxText(cbxFrom, config.FromStationName);
            }

            if (config.ToStationName != null)
            {
                setCbxText(cbxTo, config.ToStationName);
            }
            getLoginImgCode();

            addLog("初始化完成。", true);


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(Helper.AcceptAllCertificate);
            dgvQuery.AutoGenerateColumns = false;

            initSeat();
            dtpDate.Value = DateTime.Now + new TimeSpan(19, 0, 0, 0);
            loginForm.PostCallBack += new PostCallBackHandler(loginForm_PostCallBack);
            yudingForm.PostCallBack += new PostCallBackHandler(yudingForm_PostCallBack);
            commitForm.PostCallBack += new PostCallBackHandler(commitForm_PostCallBack);
            checkForm.PostCallBack += new PostCallBackHandler(checkForm_PostCallBack);
            Config config = Config.getConfig();
            string username = config.UserName;
            string password = config.Password;
            if (username != null && password != null)
            {
                txtUserName.Text = username;
                txtPassword.Text = password;
            }
            if (config.TrainRe != null)
            {
                txtTrainNameRe.Text = config.TrainRe;
            }
            Thread t = new Thread(new ThreadStart(init));
            t.Start();

        }


        private void reLogin()
        {
            addLog("已不在线!", true);
            getLoginImgCode();
            setLable(lblLoginError, "【未登录】");
            isLogin = false;
        }



        private void setCbxStations()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new void__void(setCbxStations));
            }
            else
            {
                foreach (CbxItem item in stations)
                {
                    cbxFrom.Items.Add(item);
                    cbxTo.Items.Add(item);
                }
            }
        }
        private void getStations()
        {
            string str = null;
            while (str == null)
            {
                str = getText("https://dynamic.12306.cn/otsweb/js/common/station_name.js", false);
                if (str == null)
                {
                    addLog("获取车站信息错误，稍候重试", true);
                    Thread.Sleep(500);
                }
            }

            string[] buffer = str.Split('|');
            stations.Clear();
            for (int i = 0; i < buffer.Length - 5; i += 5)
            {
                CbxItem item = new CbxItem();
                string name = buffer[i + 1];
                string code = buffer[i + 2];
                item.Name = name;
                item.code = code;
                stations.Add(item);

            }
            stations.Sort();
            setCbxStations();

        }

        private void cbx_TextChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.Text != "")
            {
                for (int i = 0; i < stations.Count; i++)
                {
                    if (stations[i].Name == cbx.Text)
                    {
                        cbx.SelectedIndex = i;
                        cbx.Select(cbx.Text.Length, 1);
                    }
                }

            }
        }
        int QueryCount = 0;

        bool queryCanRun = false;

        class queryParm
        {
            public bool Loop { get; set; }
            public CookieContainer UseCookie { get; set; }
        }
        private void query(object o)
        {
            bool loop = (bool)o;
            if (loop)
            {
                foreach (CookieContainer c in CookiesManage.CookiesList)
                {
                    Thread t = new Thread(new ParameterizedThreadStart(runQuery));
                    queryParm parm = new queryParm();
                    parm.Loop = loop;
                    parm.UseCookie = c;
                    t.Start(parm);
                    Thread.Sleep(1000);
                }

            }
            else
            {
                Thread t = new Thread(new ParameterizedThreadStart(runQuery));
                queryParm parm = new queryParm();
                parm.Loop = loop;
                parm.UseCookie = CookiesManage.CurrCookies;
                t.Start(parm);
            }

        }
        object runQuerLcok = new object();
        private void runQuery(object o)
        {
            List<TrainInfo> trainList = new List<TrainInfo>();
            Helper.Timeout = 5000;
            queryParm parm = (queryParm)o;
            bool loop = parm.Loop;
            while (queryCanRun)
            {
                addLog("查询车次:" + QueryCount, true);
                string date = dtpDate.Value.ToString("yyyy-MM-dd");

                string search = null;
                string url = "https://dynamic.12306.cn/otsweb/order/querySingleAction.do?method=queryLeftTicket&orderRequest.train_date=" +
                    date + "&orderRequest.from_station_telecode=" + select_from + "&orderRequest.to_station_telecode=" + select_to +
                    "&orderRequest.train_no=&trainPassType=QB&trainClass=QB%23D%23Z%23T%23K%23QT%23&includeStudent=00&seatTypeAndNum=&orderRequest.start_time_str=00%3A00--24%3A00";
                while (search == null)
                {
                    search = getText(url, false, parm.UseCookie);
                    if (search == null)
                    {
                        Thread.Sleep(500);
                    }
                }
                if (search == "-10")
                {
                    //reLogin();
                    return;
                }

                search = search.Replace("\\n", "\r\n");
                search = search.Replace("&nbsp;", "");
                DataTable table = new DataTable();

                using (StringReader reader = new StringReader(search))
                {
                    string line;
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
                    table.Columns.Add("train_code", typeof(string));
                    table.Columns.Add("from_code", typeof(string));
                    table.Columns.Add("to_code", typeof(string));
                    table.Columns.Add("pass", typeof(string));
                    table.Columns.Add("has", typeof(string));
                    Regex reg = new Regex("<.*?>", RegexOptions.Singleline);

                    trainList.Clear();
                    TrainInfo.TrainNameRe = txtTrainNameRe.Text;
                    while ((line = reader.ReadLine()) != null)
                    {
                        DataRow row = table.NewRow();
                        string[] strs = line.Split(',');

                        for (int i = 0; i < strs.Length; i++)
                        {
                            string item = strs[i];
                            Regex commit = new Regex("onStopHover\\('(.*?)'\\)", RegexOptions.Singleline);
                            Match mc = commit.Match(item);
                            string commitstr = null;
                            if (mc.Success)
                            {
                                commitstr = mc.Groups[1].Value;
                                string[] code = commitstr.Split('#');
                                row["train_code"] = code[0];
                                row["from_code"] = code[1];
                                row["to_code"] = code[2];
                            }

                            Regex reg_pass = new Regex("getSelected\\('(.*?)'\\)", RegexOptions.Singleline);
                            Match mp = reg_pass.Match(item);

                            if (mp.Success)
                            {
                                row["pass"] = mp.Groups[1].Value;
                                string yuding_pass = row["pass"].ToString();
                                addLog(yuding_pass, false);
                                TrainInfo info = new TrainInfo(yuding_pass);
                                if (info.Succuss())
                                {
                                    trainList.Add(new TrainInfo(yuding_pass));
                                }
                                row["has"] = "可预订";
                            }

                            item = reg.Replace(item, "");

                            row[i] = item;
                        }
                        table.Rows.Add(row);

                    }
                    table.AcceptChanges();
                }

                setQueryDataSource(table);

                //trainList.Clear();
                //trainList.Add(new TrainInfo("K758#22:37#15:18#400000K75806#LYF#GZQ#13:55#洛阳#广州#01#20#1*****30024*****00001*****00003*****0000#476DAB1763DC5855E12A08A0F84FFC5555C6A8EE053F0FF3B46E0F88#F1"));
                if (loop)
                {

                    trainList.Sort();
                    if (trainList.Count > 0 && (!cbxCheckCount.Checked || trainList[trainList.Count - 1].TicketCount > 0))
                    {
                        if (queryCanRun)
                        {
                            lock (runQuerLcok)
                            {
                                if (queryCanRun)
                                {
                                    currTrainInfo = trainList[trainList.Count - 1];
                                    TrainInfo.Date = dtpDate.Value.ToString("yyyy-MM-dd");
                                    addLog("开始预订:" + currTrainInfo.TrainName + ",余票:" + currTrainInfo.TicketCount, true);
                                    //find = true;
                                    queryCanRun = false;
                                    //Thread t = new Thread(new ThreadStart(play));
                                    //t.Start();
                                    CookiesManage.SetCurrCookie(parm.UseCookie);
                                    yuding();
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        addLog("未找到符合的车次", true);
                    }
                }

                //if (loop && queryCanRun)
                //{
                //    QueryCount++;
                //    Thread.Sleep(500);
                //    Thread t = new Thread(new ParameterizedThreadStart(runQuery));
                //    t.Start(parm);
                //}
                if (!loop)
                    break;
                QueryCount++;
                Thread.Sleep(500);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (select_from == null)
            {
                MessageBox.Show("请选择出发站");
                return;
            }
            if (select_to == null)
            {
                MessageBox.Show("请选择目的站");
                return;
            }
            queryCanRun = true;
            QueryCount = 0;
            query(false);
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

        private void selectTrain()
        {
            if (dgvQuery.SelectedRows.Count > 0)
            {
                DataRowView obj = (DataRowView)dgvQuery.SelectedRows[0].DataBoundItem;

                string train_name = obj["车次"].ToString();

                string yuding_pass = obj["pass"].ToString();

                txtTrainNameRe.Text = train_name;
            }
        }

        private void dgvQuery_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            selectTrain();
        }
        private string logFileName = null;
        private void addLog(string log, bool show)
        {
            try
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke(new void_string_bool(addLog), log, show);
                }
                else
                {

                    string str = "*" + DateTime.Now.ToString("HH:mm:ss") + " " + log + "\r\n";
                    if (logFileName == null)
                    {
                        logFileName = "log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    }
                    File.AppendAllText(logFileName, str);
                    if (show)
                    {
                        txtLog.AppendText(str);
                        txtLog.Select(txtLog.Text.Length, 0);
                        txtLog.ScrollToCaret();
                    }
                }
            }
            catch
            { }
        }
        private void yuding()
        {

            Helper.Timeout = 15000;
            string yuanshi =
@"station_train_code#D624
train_date#2013-02-08
seattype_num#
from_station_telecode#ZZF
to_station_telecode#AOH
include_student#00
from_station_telecode_name#郑州
to_station_telecode_name#上海
round_train_date#2013-02-08
round_start_time_str#00:00--24:00
single_round_type#1
train_pass_type#QB
train_class_arr#QB#D#Z#T#K#QT#
start_time_str#00:00--24:00
lishi#06:42
train_start_time#12:38
trainno4#380000D62403
arrive_time#19:20
from_station_name#郑州
to_station_name#上海虹桥
from_station_no#01
to_station_no#11
ypInfoDetail#O*****0277O*****3084M*****0005
mmStr#56326EECB4B30BBE96B87789A32AE962E6AEF605F7F0C5C16AE6F6C0
locationCode#F1";
            //string tem = getText("https://dynamic.12306.cn/otsweb/order/querySingleAction.do?method=queryaTrainStopTimeByTrainNo&train_no=" + train_code + "&from_station_telecode=" + from_code + "&to_station_telecode=" + to_code + "&depart_date=" + dtpDate.Value.ToString("yyyy-MM-dd"), false);

            using (StringReader reader = new StringReader(yuanshi))
            {
                string line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] tags = line.Split('#');
                    yudingForm.setTagValue(tags[0], tags[1]);
                }
            }
            string[] commsp = currTrainInfo.Yuanshi.Split('#');
            string[] setField = new string[] { "station_train_code", "lishi", "train_start_time", "trainno4", "from_station_telecode", 
                "to_station_telecode", "arrive_time", "from_station_name","to_station_name","from_station_no","to_station_no",
            "ypInfoDetail","mmStr","locationCode"};
            for (int i = 0; i < setField.Length; i++)
            {
                yudingForm.setTagValue(setField[i], commsp[i]);
            }
            yudingForm.setTagValue("from_station_telecode_name", yudingForm.getTagValue("from_station_name"));
            yudingForm.setTagValue("to_station_telecode_name", yudingForm.getTagValue("to_station_name"));
            yudingForm.setTagValue("train_date", TrainInfo.Date);
            yudingForm.setTagValue("round_train_date", TrainInfo.Date);
            File.WriteAllText("yuding.txt", yudingForm.debug());
            yudingForm.post();


        }
        void yudingForm_PostCallBack(HtmlForm sender, string result)
        {
            if (result.IndexOf("登录名") != -1)
            {
                reLogin();
            }
            else if (result.IndexOf("系统忙") != -1)
            {
                if (!queryCanRun)
                {
                    addLog("系统忙，稍候重试", true);
                    Thread.Sleep(1000);
                    yuding();
                }
            }
            else
            {
                if (!queryCanRun)
                {
                    getCommitPage();
                }
            }

        }
        string lefttick = null;
        string token = null;
        DateTime getCommitTime;

        private void getCommitPage()
        {

            addLog("getCommitPage", true);
            string result = null;
            while (result == null)
            {
                result = getText("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=init", false);
                if (result == null)
                    Thread.Sleep(500);
            }
            getCommitTime = DateTime.Now;
            if (result.IndexOf("系统忙") != -1)
            {
                if (!queryCanRun)
                {
                    addLog("系统忙，稍候重试。", true);
                    Thread.Sleep(3000);
                    queryCanRun = true;
                    getCommitPage();
                }
            }
            else
            {

                //string tem1 = getText("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=getpassengerJson", true);
                //addLog(tem1);

                //getCommitImgCode();

                Regex leftTicketStrReg = new Regex("<input.*?name=\"leftTicketStr\".*?value=\"(.*?)\".*?/>", RegexOptions.Singleline);
                Match leftM = leftTicketStrReg.Match(result);

                if (leftM.Success)
                {
                    lefttick = leftM.Groups[1].Value;
                }

                Regex tokenReg = new Regex("<input.*?name=\"org\\.apache\\.struts\\.taglib\\.html\\.TOKEN\".*?value=\"(.*?)\".*?/>", RegexOptions.Singleline);
                Match tokenM = tokenReg.Match(result);
                initSelectedPassenger();
                if (tokenM.Success)
                {
                    token = tokenM.Groups[1].Value;
                }
                if (lefttick != null && token != null)
                {
                    getCommitImgCode();
                }
            }
        }

        private void commit(string lefttick, string token, string code)
        {
            canPlaySound = false;
            initSelectedPassenger();
            TrainInfo.Date = dtpDate.Value.ToString("yyyy-MM-dd");
            string yuanshi =
@"org.apache.struts.taglib.html.TOKEN#1f0aa644944e5073ff550f8cb51df8908
leftTicketStr#O023650277O023653084M037950005
textfield#中文或拼音首字母
orderRequest.train_date#2013-02-08
orderRequest.train_no#380000D62403
orderRequest.station_train_code#D624
orderRequest.from_station_telecode#ZZF
orderRequest.to_station_telecode#AOH
orderRequest.seat_type_code#
orderRequest.ticket_type_order_num#
orderRequest.bed_level_order_num#000000000000000000000000000000
orderRequest.start_time#12:38
orderRequest.end_time#19:20
orderRequest.from_station_name#郑州
orderRequest.to_station_name#上海虹桥
orderRequest.cancel_flag#1
orderRequest.id_mode#Y";

            commitForm.QueryString.Clear();

            commitForm.QueryString.Add(new KeyValue("rand", code));
            commitForm.ClearTag();


            using (StringReader reader = new StringReader(yuanshi))
            {

                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tags = line.Split('#');
                    commitForm.setTagValue(tags[0], tags[1]);
                }
            }
            int i = 0;
            for (; i < selectedPassengers.Count; i++)
            {
                passengerTicketItem item = selectedPassengers[i];
                item.Index = i + 1;
                item.addToForm(commitForm);
            }

            passengerTicketItem empty = new passengerTicketItem(3);

            for (; i < 5; i++)
            {
                empty.addToForm(commitForm);
            }



            string yushi2 =
@"randCode#cccc
orderRequest.reserve_flag#A
tFlag#dc";

            using (StringReader reader = new StringReader(yushi2))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tags = line.Split('#');
                    commitForm.setTagValue(tags[0], tags[1]);
                }
            }

            commitForm.setTagValue("orderRequest.train_date", TrainInfo.Date);
            commitForm.setTagValue("orderRequest.train_no", currTrainInfo.TrainCode);
            commitForm.setTagValue("orderRequest.station_train_code", currTrainInfo.TrainName);
            commitForm.setTagValue("orderRequest.from_station_telecode", currTrainInfo.FromStationCode);
            commitForm.setTagValue("orderRequest.to_station_telecode", currTrainInfo.TotationCode);
            commitForm.setTagValue("orderRequest.from_station_name", currTrainInfo.FromStationName);
            commitForm.setTagValue("orderRequest.to_station_name", currTrainInfo.ToStationName);
            commitForm.setTagValue("orderRequest.start_time", currTrainInfo.StartTime);
            commitForm.setTagValue("orderRequest.end_time", currTrainInfo.ArriveTime);
            commitForm.setTagValue("org.apache.struts.taglib.html.TOKEN", token);
            commitForm.setTagValue("leftTicketStr", lefttick);
            commitForm.setTagValue("randCode", code);
            //getImage("https://dynamic.12306.cn/otsweb/images/long_button_u_over.jpg", "https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=init");
            File.WriteAllText("commit.txt", commitForm.debug());
            commitForm.post();
        }
        private bool getTickCount()
        {
            try
            {
                string url = "https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=getQueueCount&train_date=" +
                        TrainInfo.Date + "&train_no=" + currTrainInfo.TrainCode + "&station=" + currTrainInfo.TrainName + "&seat=" + TrainInfo.Seat + "&from=" + currTrainInfo.FromStationCode + "&to=" + currTrainInfo.TotationCode + "&ticket=" + lefttick;
                string traincount = null;
                while (traincount == null)
                {
                    traincount = getText(url, false);
                    if (traincount == null)
                        Thread.Sleep(500);
                }
                addLog(traincount, true);
                int waiteCount = 0;
                JObject tcjson = (JObject)JsonConvert.DeserializeObject(traincount);
                object waito = tcjson["count"];
                object op_2 = tcjson["op_2"];
                if (waito != null)
                {
                    waiteCount = Convert.ToInt32(waito.ToString());
                }
                //return new int[] { tickCount, waiteCount };
                addLog("排队人数：" + waiteCount, true);
                if (op_2 != null && Convert.ToBoolean(op_2.ToString()))
                {
                    addLog("不允许排队，稍候重试", true);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }
        void commitForm_PostCallBack(HtmlForm sender, string result)
        {
            if (result.IndexOf("登录名") != -1)
            {
                reLogin();
                return;
            }
            if (result.IndexOf("网络可能存在问题") != -1)
            {
                if (!queryCanRun)
                {
                    addLog("网络错误，稍候重试", true);
                    Thread.Sleep(1000);
                    commit(lefttick, token, txtCommitCode.Text);
                }
            }
            JObject o = (JObject)JsonConvert.DeserializeObject(result);
            object error = o["errMsg"];
            object msg = o["msg"];
            object checkHuimd = o["checkHuimd"];
            object check608 = o["check608"];
            addLog(result, true);
            if (checkHuimd != null && checkHuimd.ToString() == ("Y") &&
                check608 != null && check608.ToString() == ("Y") &&
                error != null && error.ToString() == ("Y"))
            {
                //getImage("https://dynamic.12306.cn/otsweb/images/long_button_x.jpg", "https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=init");
                if (getTickCount())
                {
                    Thread.Sleep(checkDelay);//重点
                    checkTick(lefttick, token, txtCommitCode.Text);
                }
                else
                {
                    if (!queryCanRun)
                    {
                        Thread.Sleep(3000);
                        commit(lefttick, token, txtCommitCode.Text);
                    }
                }

            }
            else
            {
                if (error != null && error.ToString() != "Y")
                {
                    addLog(error.ToString(), true);
                }
                if (msg != null && msg.ToString() != "")
                {
                    addLog(msg.ToString(), true);
                }
                if (error != null && error.ToString().IndexOf("验证码") != -1)
                {
                    getCommitImgCode();
                }
                else if (msg != null && msg.ToString().IndexOf("取消次数过多") != -1)
                {
                    MessageBox.Show("取消次数过多，无法购票");
                }
                else
                {
                    if (!queryCanRun)
                    {
                        commit(lefttick, token, txtCommitCode.Text);
                    }
                }

            }
        }
        private void checkTick(string lefttick, string token, string code)
        {
            string yuanshi =
@"org.apache.struts.taglib.html.TOKEN#1f0aa644944e5073ff550f8cb51df8908
leftTicketStr#O023650277O023653084M037950005
textfield#中文或拼音首字母
orderRequest.train_date#2013-02-08
orderRequest.train_no#380000D62403
orderRequest.station_train_code#D624
orderRequest.from_station_telecode#ZZF
orderRequest.to_station_telecode#AOH
orderRequest.seat_type_code#
orderRequest.ticket_type_order_num#
orderRequest.bed_level_order_num#000000000000000000000000000000
orderRequest.start_time#12:38
orderRequest.end_time#19:20
orderRequest.from_station_name#郑州
orderRequest.to_station_name#上海虹桥
orderRequest.cancel_flag#1
orderRequest.id_mode#Y";

            checkForm.ClearTag();

            using (StringReader reader = new StringReader(yuanshi))
            {
                string line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] tags = line.Split('#');
                    checkForm.AddTag(tags[0], tags[1]);
                }
            }
            int i = 0;
            for (; i < selectedPassengers.Count; i++)
            {
                passengerTicketItem item = selectedPassengers[i];
                item.Index = i + 1;
                item.addToForm(checkForm);
            }

            passengerTicketItem empty = new passengerTicketItem(3);

            for (; i < 5; i++)
            {
                empty.addToForm(checkForm);
            }

            string yushi2 =
@"randCode#cccc
orderRequest.reserve_flag#A";

            using (StringReader reader = new StringReader(yushi2))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tags = line.Split('#');
                    checkForm.AddTag(tags[0], tags[1]);
                }
            }

            checkForm.setTagValue("orderRequest.train_date", TrainInfo.Date);
            checkForm.setTagValue("orderRequest.train_no", currTrainInfo.TrainCode);
            checkForm.setTagValue("orderRequest.station_train_code", currTrainInfo.TrainName);
            checkForm.setTagValue("orderRequest.from_station_telecode", currTrainInfo.FromStationCode);
            checkForm.setTagValue("orderRequest.to_station_telecode", currTrainInfo.TotationCode);
            checkForm.setTagValue("orderRequest.from_station_name", currTrainInfo.FromStationName);
            checkForm.setTagValue("orderRequest.to_station_name", currTrainInfo.ToStationName);
            checkForm.setTagValue("orderRequest.start_time", currTrainInfo.StartTime);
            checkForm.setTagValue("orderRequest.end_time", currTrainInfo.ArriveTime);
            checkForm.setTagValue("org.apache.struts.taglib.html.TOKEN", token);
            checkForm.setTagValue("leftTicketStr", lefttick);
            checkForm.setTagValue("randCode", code);
            File.WriteAllText("post_check.txt", checkForm.debug());
            checkForm.post();
        }
        void checkForm_PostCallBack(HtmlForm sender, string result)
        {
            if (result.IndexOf("登录名") != -1)
            {
                reLogin();
                return;
            }

            try
            {
                JObject o = (JObject)JsonConvert.DeserializeObject(result);
                string error = o["errMsg"].ToString();
                addLog("check:" + error, true);
                if (error.IndexOf("验证码") != -1)
                {
                    getCommitImgCode();

                }
                else if (error.IndexOf("非法") != -1)
                {
                    if (!queryCanRun)
                    {
                        Thread.Sleep(3000);
                        commit(lefttick, token, txtCommitCode.Text);
                    }
                }
                else if (error.IndexOf("重复提交") != -1)
                {
                    if (!queryCanRun)
                    {
                        addLog("##########################", true);
                        getCommitPage();
                    }
                }
                else if (error.IndexOf("已超过余票数") != -1)
                {
                    if (!queryCanRun)
                    {
                        Thread.Sleep(3000);
                        commit(lefttick, token, txtCommitCode.Text);
                    }
                }
                else if (error.IndexOf("未付款订单") != -1)
                {
                    OpenIE openite = new OpenIE("包含未付款订单,快去付款！");
                    openite.ShowDialog();
                    //MessageBox.Show("包含未付款订单,快去付款！");
                }
                else if (error == "Y")
                {
                    addLog("成功提交订单", true);
                    getOrder(true);
                }
            }
            catch
            {
                if (!queryCanRun)
                {
                    getCommitPage();
                }
            }
        }
        private void getOrder(bool reQuery)
        {
            try
            {
                while (true)
                {
                    string tem = getText("https://dynamic.12306.cn/otsweb/order/myOrderAction.do?method=queryOrderWaitTime&tourFlag=dc", false);
                    JObject json = (JObject)JsonConvert.DeserializeObject(tem);
                    object oWaiteTime = json["waitTime"];
                    object oWaiteCount = json["waitCount"];
                    object oOrderId = json["orderId"];
                    object msg = json["msg"];
                    if (oWaiteTime != null)
                    {
                        int waiteTime = Convert.ToInt32(oWaiteTime.ToString());
                        int count = Convert.ToInt32(oWaiteCount.ToString());
                        TimeSpan span = new TimeSpan(0, 0, waiteTime);
                        string strSpan = ((int)span.TotalMinutes).ToString() + "分钟" + span.Seconds + "秒";
                        if (waiteTime >= 0)
                        {
                            addLog("排队时间：" + strSpan + " 排队人数：" + count, true);
                        }
                        else
                        {
                            if (waiteTime == -1)
                            {
                                addLog("购票成功，订单号：" + oOrderId, true);
                                OpenIE open = new OpenIE("购票成功，订单号：" + oOrderId + "。快去付款！");
                                open.ShowDialog();

                            }
                            else if (waiteTime == -2)
                            {
                                addLog("出票失败:" + msg + ",重新购票.", true);
                                if (reQuery && !queryCanRun)
                                {
                                    QueryCount = 0;
                                    queryCanRun = true;
                                    setImage(pcxCommitCode, null);
                                    getCommitPage();
                                }
                            }
                            else if (waiteTime == -3)
                            {
                                addLog("订单已经被取消！", true);
                            }
                            else if (waiteTime == -4)
                            {
                                addLog("正在处理中....", true);
                            }
                            break;
                        }
                    }
                    else
                    {
                        addLog("未知状态", true);
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            catch
            {
                addLog("未知状态", true);
            }
        }
        private void initSelectedPassenger()
        {

            selectedPassengers.Clear();

            StringBuilder sb = new StringBuilder();
            foreach (int index in cbxListPassenger.CheckedIndices)
            {
                passengerTicketItem item = (passengerTicketItem)cbxListPassenger.Items[index];
                item.Seat = TrainInfo.Seat;
                selectedPassengers.Add(item);
                addLog("购票人：" + item.getInfo(), true);
            }
            Config.SaveInfo();

        }
        private void btnYuding_Click(object sender, EventArgs e)
        {
            canPlaySound = false;
            if (select_from == null)
            {
                MessageBox.Show("请选择出发站");
                return;
            }
            if (select_to == null)
            {
                MessageBox.Show("请选择目的站");
                return;
            }
            if (cbxListPassenger.CheckedIndices.Count == 0)
            {
                MessageBox.Show("请选择乘车人");
                return;
            }
            QueryCount = 0;
            queryCanRun = true;
            Thread t = new Thread(new ParameterizedThreadStart(query));
            t.Start(true);
        }
        string select_from;
        string select_to;
        private void cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config config = Config.getConfig();
            if (cbxFrom.SelectedIndex != -1)
            {
                select_from = stations[cbxFrom.SelectedIndex].code;
                config.FromStationName = stations[cbxFrom.SelectedIndex].Name;
            }
            else
            {
                select_from = null;
            }
            if (cbxTo.SelectedIndex != -1)
            {
                select_to = stations[cbxTo.SelectedIndex].code;
                config.ToStationName = stations[cbxTo.SelectedIndex].Name;
            }
            else
            {
                select_to = null;
            }
            Config.SaveInfo();


        }

        private void pcxLoginImgCode_Click(object sender, EventArgs e)
        {
            getLoginImgCode();
        }

        private void cbxSeat_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrainInfo.Seat = ((CbxItem)cbxSeat.Items[cbxSeat.SelectedIndex]).code;
            Config config = Config.getConfig();
            config.SeatIndex = cbxSeat.SelectedIndex;
            Config.SaveInfo();
        }
        bool delayCommitRuning = false;
        private void delayCommit()
        {
            try
            {
                delayCommitRuning = true;
                TimeSpan delay = commitDelay - (DateTime.Now - getCommitTime);
                while (delay.TotalMilliseconds > 0)
                {
                    setLable(lblDealy, delay.TotalSeconds + "秒后提交");

                    Thread.Sleep(10);
                    delay = commitDelay - (DateTime.Now - getCommitTime);
                }
                setLable(lblDealy, "");
                commit(lefttick, token, txtCommitCode.Text);
            }
            finally
            {
                delayCommitRuning = false;
            }
        }
        private void txtCommitCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCommitCode.Text == " ")
            {
                getCommitImgCode();
            }
            else if (txtCommitCode.Text.Length == 4)
            {
                if (lefttick != null && token != null)
                {

                    TimeSpan span = DateTime.Now - getCommitTime;
                    if (span < commitDelay)
                    {
                        if (!delayCommitRuning)
                            new Thread(new ThreadStart(delayCommit)).Start();
                    }
                    else
                    {
                        commit(lefttick, token, txtCommitCode.Text);
                    }
                }
                else
                {
                    if (!queryCanRun)
                    {
                        getCommitPage();
                    }
                }
            }
        }

        private void pcxCommitCode_Click(object sender, EventArgs e)
        {
            getCommitImgCode();
        }

        private void txtLoginImgCode_TextChanged(object sender, EventArgs e)
        {
            if (txtLoginImgCode.Text == " ")
            {
                getLoginImgCode();
            }
            else if (txtLoginImgCode.Text.Length == 4)
            {
                login();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            QueryCount = 0;
            queryCanRun = false;
        }

        private void txtTrainNameRe_Leave(object sender, EventArgs e)
        {
            Config config = Config.getConfig();
            if (txtTrainNameRe.Text != config.TrainRe)
            {
                config.TrainRe = txtTrainNameRe.Text;
                Config.SaveInfo();
            }
        }

        private void btnOpenID_Click(object sender, EventArgs e)
        {
            Helper.OpenIE();
        }

        private void cbxListPassenger_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config config = Config.getConfig();
            config.SelectPassengers.Clear();

            foreach (int index in cbxListPassenger.CheckedIndices)
            {
                passengerTicketItem item = (passengerTicketItem)cbxListPassenger.Items[index];
                config.SelectPassengers.Add(item.Name);
            }
            Config.SaveInfo();
        }

        bool canPlaySound = false;
        private void play()
        {
            canPlaySound = true;
            while (canPlaySound)
            {
                SystemSounds.Asterisk.Play();
                Thread.Sleep(500);
            }
        }
        private void createNewCookie()
        {
            CookiesManage.AddCookie();
            Thread t = new Thread(new ThreadStart(reLogin));
            t.Start();
            lblCookieCount.Text = CookiesManage.CookiesList.Count.ToString();
        }
        private void btnAddCookie_Click(object sender, EventArgs e)
        {
            createNewCookie();
        }

        private void txtLoginImgCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isLogin)
            {
                createNewCookie();
            }
        }


    }
}
