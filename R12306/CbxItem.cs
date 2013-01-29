using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R12306
{
    class CbxItem:IComparable
    {
        public string Name;
        public string code;
        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            return this.Name.CompareTo(((CbxItem)obj).Name);
        }

        public CbxItem(string name, string code)
        {
            this.Name = name;
            this.code = code;
        }

        public CbxItem()
        {
 
        }
    }
}
