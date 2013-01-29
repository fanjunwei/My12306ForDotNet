using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace R12306
{
    class passengerTicketItem
    {
        private static Hashtable _cardTypeArray = null;

        public static Hashtable CardTypeArray
        {
            get
            {
                if (_cardTypeArray == null)
                {
                    _cardTypeArray = new Hashtable();
                    _cardTypeArray.Add("1", "二代身份证");
                    _cardTypeArray.Add("2", "一代身份证");
                    _cardTypeArray.Add("C", "港澳通行证");
                    _cardTypeArray.Add("G", "台湾通行证");
                    _cardTypeArray.Add("B", "护照");
                }
                return passengerTicketItem._cardTypeArray;
            }

        }
        private static Hashtable _ticketTypeArray = null;

        public static Hashtable TicketTypeArray
        {
            get
            {
                if (_ticketTypeArray == null)
                {
                    _ticketTypeArray = new Hashtable();
                    _ticketTypeArray.Add("1", "成人票");
                    _ticketTypeArray.Add("2", "儿童票");
                    _ticketTypeArray.Add("3", "学生票");
                    _ticketTypeArray.Add("4", "残军票");
                }
                return passengerTicketItem._ticketTypeArray;
            }
        }

        public passengerTicketItem(int index)
        {
            this.Index = index;

        }
        /// <summary>
        /// 席别
        /// </summary>
        public string Seat { get; set; }
        /// <summary>
        /// 票种
        /// </summary>
        public string Ticket { get; set; }

        public string Name { get; set; }
        public string Cardtype { get; set; }
        public string Cardno { get; set; }
        public string Mobileno { get; set; }
        public int Index { get; set; }


        public void addToForm(HtmlForm form)
        {
            if (Name != null && Name != "")
            {
                form.AddTag("passengerTickets", Seat + ",0," + Ticket + "," + Name + "," + Cardtype + "," + Cardno + "," + Mobileno + ",N");
                form.AddTag("oldPassengers", "");
                form.AddTag("passenger_" + Index + "_seat", Seat);
                form.AddTag("passenger_" + Index + "_ticket", Ticket);
                form.AddTag("passenger_" + Index + "_name", Name);
                form.AddTag("passenger_" + Index + "_cardtype", Cardtype);
                form.AddTag("passenger_" + Index + "_cardno", Cardno);
                form.AddTag("passenger_" + Index + "_mobileno", Mobileno);
            }
            else
            {

                form.AddTag("oldPassengers", "");
                form.AddTag("checkbox9", "Y");
            }

        }
        public override string ToString()
        {
            return Name;
        }

        public string getInfo()
        {
            return TicketTypeArray[Ticket] + " " + Name + " " + CardTypeArray[Cardtype] + " " + Cardno + " " + Mobileno;
        }
    }
}
