using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R12306
{
    public class KeyValue
    {
        
        public KeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public KeyValue(HtmlForm form, string key, string value, string type)
        {
            this.Key = key;
            this.Value = value;
            this.TagType = type;
            this._form = form;
        }
        private HtmlForm _form;
        private string _key;
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
               
            }
        }
        public string TagType { get; set; }
 
        public HtmlForm getForm()
        {
            return _form;
        }
        public override string ToString()
        {
   
                return Key + "=" + Value;
           

        }

       
    }
}
