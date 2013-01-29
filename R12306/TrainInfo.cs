using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace R12306
{
    class TrainInfo : IComparable
    {
        public static string TrainNameRe { get; set; }
        public static string Seat { get; set; }
        public static string Date { get; set; }


        public string TrainCode { get; private set; }
        public string TrainName { get; private set; }
        public string StartTime { get; private set; }
        public string ArriveTime { get; private set; }
        public string FromStationName { get; private set; }
        public string FromStationCode { get; private set; }
        public string ToStationName { get; private set; }
        public string TotationCode { get; private set; }
        public string Yuanshi { get; private set; }

        private Hashtable ticketCouts = new Hashtable();
        private Hashtable info = new Hashtable();

        public TrainInfo(string yuanshi)
        {
            Yuanshi = yuanshi;
            //T54#10:08#04:18#9300000T5465#ZZF#SHH#14:26#郑州#上海#14#24#1*****30814*****00061*****00003*****0023#D658142B10587A5920095D24A7DF5B6E745EFDE3F13814C95E940261#R1
            string[] commsp = yuanshi.Split('#');
            string[] setField = new string[] { "station_train_code", "lishi", 
                "train_start_time", "trainno4", "from_station_telecode", 
                "to_station_telecode", "arrive_time", "from_station_name",
                "to_station_name","from_station_no","to_station_no",
                "ypInfoDetail","mmStr","locationCode"};

            for (int i = 0; i < setField.Length; i++)
            {
                info[setField[i]] = commsp[i];
            }

            TrainCode = info["trainno4"].ToString();
            TrainName = info["station_train_code"].ToString();
            StartTime = info["train_start_time"].ToString();
            ArriveTime = info["arrive_time"].ToString();
            FromStationName = info["from_station_name"].ToString();
            FromStationCode = info["from_station_telecode"].ToString();
            ToStationName = info["to_station_name"].ToString();
            TotationCode = info["to_station_telecode"].ToString();
            ticketCouts = getCount(info["ypInfoDetail"].ToString());


        }
        public  static Hashtable getCount(string ypInfoDetail)
        {
            Hashtable table = new Hashtable();
            Regex reZuo = new Regex(@"(.)\*\*\*\*\*0(\d\d\d)");
            //MatchCollection mZuos = reZuo.Matches(ypInfoDetail);
            Match m = reZuo.Match(ypInfoDetail);
            while (m != null && m.Success)
            {
                string seat = m.Groups[1].Value;
                int count = Convert.ToInt32(m.Groups[2].Value);
                table[seat] = count;
                m = m.NextMatch();
            }

            Regex rewu = new Regex(@"(.)\*\*\*\*\*3(\d\d\d)");
            Match mwu = rewu.Match(ypInfoDetail);
            if (mwu.Success)
            {
                int count1 = Convert.ToInt32(mwu.Groups[2].Value);
                table["无座"] = count1;
            }
            return table;

        }
        public bool Succuss()
        {
            Regex re = new Regex(TrainNameRe);
            if (re.Match(this.TrainName).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int TicketCount
        {
            get
            {
                object o1 = this.ticketCouts[Seat];
                if (o1 != null)
                {
                    return Convert.ToInt32(o1);
                }
                else
                {
                    return 0;
                }
            }
        }
        public int CompareTo(object obj)
        {

            object o1 = this.ticketCouts[Seat];
            object o2 = ((TrainInfo)obj).ticketCouts[Seat];
            int count1;
            int count2;

            if (!this.Succuss())
            {
                count1 = -1;
            }
            else
            {
                count1 = Convert.ToInt32(o1);
            }

            if (!((TrainInfo)obj).Succuss())
            {
                count2 = -1;
            }
            else
            {
                count2 = Convert.ToInt32(o2);
            }


            return count1.CompareTo(count2);

        }
    }
}
