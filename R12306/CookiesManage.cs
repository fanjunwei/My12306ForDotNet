using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace R12306
{
    class CookiesManage
    {

        public static List<CookieContainer> CookiesList = new List<CookieContainer>();
        public static int CookieIndex = -1;
        private static object threadLock = new object();
        public  static CookieContainer CurrCookies
        {
            get
            {
                lock (threadLock)
                {
                    if (CookiesList.Count == 0)
                    {
                        CookiesList.Add(new CookieContainer());
                        CookieIndex = 0;
                    }
                    return CookiesList[CookieIndex];
                }
            }
        }

        public static void AddCookie()
        {
            lock (threadLock)
            {
                CookiesList.Add(new CookieContainer());
                CookieIndex = CookiesList.Count - 1;
            }
        }

        public static void SetCurrCookie(CookieContainer cookie)
        {
            lock (threadLock)
            {
                CookieIndex = CookiesList.IndexOf(cookie);
            }
        }
    }
}
