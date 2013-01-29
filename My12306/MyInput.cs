using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace My12306
{
    public partial class MyInput : UserControl
    {

        private bool _isCheckbox = false;

        public bool IsCheckbox
        {
            get { return _isCheckbox; }
            set { _isCheckbox = value; }
        }
        private string _tagName = null;
        public string tagName
        {
            get
            {
                return _tagName;
            }
            set
            {
                _tagName = value;
            }
        }
        public string tagValue
        {
            get
            {
                if (_isCheckbox)
                {
                    if (cbxValue.Checked)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                   
                }
                else
                {
                    return txtValue.Text;
                }
            }
            set
            {
                if (_isCheckbox)
                {
                    if (value == "1")
                    {
                        cbxValue.Checked = true;
                    }
                    else
                    {
                        cbxValue.Checked = false;
                    }

                }
                else
                {
                    txtValue.Text = value;
                }
            }
        }
        
        public MyInput()
        {
            InitializeComponent();
        }

        private void MyInput_Load(object sender, EventArgs e)
        {
            if (IsCheckbox)
            {
                cbxValue.Visible = true;
                txtValue.Visible = false;
            }
            else
            {
                cbxValue.Visible = false;
                txtValue.Visible = true;
            }
            lblName.Text = _tagName;
        }
    }
}
