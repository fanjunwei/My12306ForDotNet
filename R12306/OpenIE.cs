using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;

namespace R12306
{
    public partial class OpenIE : Form
    {
        string _message = null;
        public OpenIE(string messag)
        {
            this._message = messag;
            InitializeComponent();
        }
        public OpenIE()
        {
            InitializeComponent();
        }

        private void btnOpenIE_Click(object sender, EventArgs e)
        {
            Helper.OpenIE();
        }

        private void OpenIE_Load(object sender, EventArgs e)
        {
            lblMessage.Text = _message;
        }
    }
}
