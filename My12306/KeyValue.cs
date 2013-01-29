using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My12306
{
    delegate void setInputControlValueHandler(MyInput input,string value);
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
                if (inputControl != null)
                {
                    setInputControlValue(inputControl, value);
                }
            }
        }
        public string TagType { get; set; }
        public MyInput inputControl { get; set; }
        public HtmlForm getForm()
        {
            return _form;
        }
        public override string ToString()
        {
            if (TagType == "text" || TagType == "password")
            {
                if (this.inputControl != null)
                {
                    return Key + "=" + inputControl.tagValue;
                }
                else
                {
                    return null;
                }
            }
            else if (TagType == "checkbox")
            {
                if (this.inputControl != null && inputControl.tagValue == "1")
                {
                    return Key + "=" + Value;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return Key + "=" + Value;
            }

        }

        private void setInputControlValue(MyInput input, string value)
        {
            if (input.InvokeRequired)
            {
                input.Invoke(new setInputControlValueHandler(setInputControlValue), input, value);
            }
            else
            {
                input.tagValue = value;
            }
        }
    }
}
