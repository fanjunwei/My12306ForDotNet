namespace R12306
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLoginImgCode = new System.Windows.Forms.TextBox();
            this.pcxLoginImgCode = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenID = new System.Windows.Forms.Button();
            this.lblLoginError = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cbxFrom = new System.Windows.Forms.ComboBox();
            this.cbxTo = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvQuery = new System.Windows.Forms.DataGridView();
            this.btnYuding = new System.Windows.Forms.Button();
            this.cbxSeat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pcxCommitCode = new System.Windows.Forms.PictureBox();
            this.txtCommitCode = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.lblDealy = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTrainNameRe = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.cbxListPassenger = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxCheckCount = new System.Windows.Forms.CheckBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pcxLoginImgCode)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcxCommitCode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // txtUserName
            // 
            this.txtUserName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtUserName.Location = new System.Drawing.Point(76, 14);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(138, 21);
            this.txtUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码";
            // 
            // txtPassword
            // 
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPassword.Location = new System.Drawing.Point(76, 41);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(138, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "验证码";
            // 
            // txtLoginImgCode
            // 
            this.txtLoginImgCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtLoginImgCode.Location = new System.Drawing.Point(76, 68);
            this.txtLoginImgCode.Name = "txtLoginImgCode";
            this.txtLoginImgCode.Size = new System.Drawing.Size(138, 21);
            this.txtLoginImgCode.TabIndex = 5;
            this.txtLoginImgCode.TextChanged += new System.EventHandler(this.txtLoginImgCode_TextChanged);
            // 
            // pcxLoginImgCode
            // 
            this.pcxLoginImgCode.Location = new System.Drawing.Point(76, 99);
            this.pcxLoginImgCode.Name = "pcxLoginImgCode";
            this.pcxLoginImgCode.Size = new System.Drawing.Size(100, 24);
            this.pcxLoginImgCode.TabIndex = 6;
            this.pcxLoginImgCode.TabStop = false;
            this.pcxLoginImgCode.Click += new System.EventHandler(this.pcxLoginImgCode_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(76, 129);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenID);
            this.groupBox1.Controls.Add(this.lblLoginError);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.pcxLoginImgCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLoginImgCode);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 173);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // btnOpenID
            // 
            this.btnOpenID.ForeColor = System.Drawing.Color.Red;
            this.btnOpenID.Location = new System.Drawing.Point(182, 129);
            this.btnOpenID.Name = "btnOpenID";
            this.btnOpenID.Size = new System.Drawing.Size(75, 23);
            this.btnOpenID.TabIndex = 9;
            this.btnOpenID.Text = "打开IE";
            this.btnOpenID.UseVisualStyleBackColor = true;
            this.btnOpenID.Click += new System.EventHandler(this.btnOpenID_Click);
            // 
            // lblLoginError
            // 
            this.lblLoginError.AutoSize = true;
            this.lblLoginError.ForeColor = System.Drawing.Color.Red;
            this.lblLoginError.Location = new System.Drawing.Point(74, 155);
            this.lblLoginError.Name = "lblLoginError";
            this.lblLoginError.Size = new System.Drawing.Size(0, 12);
            this.lblLoginError.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(457, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "发站";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(457, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "到站";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(619, 73);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 13;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbxFrom
            // 
            this.cbxFrom.FormattingEnabled = true;
            this.cbxFrom.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbxFrom.Location = new System.Drawing.Point(492, 16);
            this.cbxFrom.Name = "cbxFrom";
            this.cbxFrom.Size = new System.Drawing.Size(121, 20);
            this.cbxFrom.TabIndex = 14;
            this.cbxFrom.SelectedIndexChanged += new System.EventHandler(this.cbx_SelectedIndexChanged);
            this.cbxFrom.TextChanged += new System.EventHandler(this.cbx_TextChanged);
            // 
            // cbxTo
            // 
            this.cbxTo.FormattingEnabled = true;
            this.cbxTo.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cbxTo.Location = new System.Drawing.Point(492, 48);
            this.cbxTo.Name = "cbxTo";
            this.cbxTo.Size = new System.Drawing.Size(121, 20);
            this.cbxTo.TabIndex = 15;
            this.cbxTo.SelectedIndexChanged += new System.EventHandler(this.cbx_SelectedIndexChanged);
            this.cbxTo.TextChanged += new System.EventHandler(this.cbx_TextChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(492, 73);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(121, 21);
            this.dtpDate.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(457, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "日期";
            // 
            // dgvQuery
            // 
            this.dgvQuery.AllowUserToAddRows = false;
            this.dgvQuery.AllowUserToDeleteRows = false;
            this.dgvQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18});
            this.dgvQuery.Location = new System.Drawing.Point(12, 186);
            this.dgvQuery.Name = "dgvQuery";
            this.dgvQuery.ReadOnly = true;
            this.dgvQuery.RowTemplate.Height = 23;
            this.dgvQuery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuery.Size = new System.Drawing.Size(747, 219);
            this.dgvQuery.TabIndex = 20;
            this.dgvQuery.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvQuery_MouseDoubleClick);
            // 
            // btnYuding
            // 
            this.btnYuding.Location = new System.Drawing.Point(461, 157);
            this.btnYuding.Name = "btnYuding";
            this.btnYuding.Size = new System.Drawing.Size(75, 23);
            this.btnYuding.TabIndex = 24;
            this.btnYuding.Text = "开始预订";
            this.btnYuding.UseVisualStyleBackColor = true;
            this.btnYuding.Click += new System.EventHandler(this.btnYuding_Click);
            // 
            // cbxSeat
            // 
            this.cbxSeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSeat.FormattingEnabled = true;
            this.cbxSeat.Location = new System.Drawing.Point(494, 127);
            this.cbxSeat.Name = "cbxSeat";
            this.cbxSeat.Size = new System.Drawing.Size(87, 20);
            this.cbxSeat.TabIndex = 25;
            this.cbxSeat.SelectedIndexChanged += new System.EventHandler(this.cbxSeat_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(459, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "席别";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 422);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "验证码";
            // 
            // pcxCommitCode
            // 
            this.pcxCommitCode.Location = new System.Drawing.Point(52, 411);
            this.pcxCommitCode.Name = "pcxCommitCode";
            this.pcxCommitCode.Size = new System.Drawing.Size(100, 33);
            this.pcxCommitCode.TabIndex = 28;
            this.pcxCommitCode.TabStop = false;
            this.pcxCommitCode.Click += new System.EventHandler(this.pcxCommitCode_Click);
            // 
            // txtCommitCode
            // 
            this.txtCommitCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtCommitCode.Location = new System.Drawing.Point(158, 416);
            this.txtCommitCode.Name = "txtCommitCode";
            this.txtCommitCode.Size = new System.Drawing.Size(100, 21);
            this.txtCommitCode.TabIndex = 29;
            this.txtCommitCode.TextChanged += new System.EventHandler(this.txtCommitCode_TextChanged);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(8, 450);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(816, 157);
            this.txtLog.TabIndex = 30;
            this.txtLog.Text = "";
            // 
            // lblDealy
            // 
            this.lblDealy.AutoSize = true;
            this.lblDealy.ForeColor = System.Drawing.Color.Red;
            this.lblDealy.Location = new System.Drawing.Point(264, 422);
            this.lblDealy.Name = "lblDealy";
            this.lblDealy.Size = new System.Drawing.Size(23, 12);
            this.lblDealy.TabIndex = 32;
            this.lblDealy.Text = "---";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(457, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 33;
            this.label9.Text = "车次";
            // 
            // txtTrainNameRe
            // 
            this.txtTrainNameRe.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.txtTrainNameRe.Location = new System.Drawing.Point(492, 100);
            this.txtTrainNameRe.Name = "txtTrainNameRe";
            this.txtTrainNameRe.Size = new System.Drawing.Size(100, 21);
            this.txtTrainNameRe.TabIndex = 34;
            this.txtTrainNameRe.Text = "D284";
            this.txtTrainNameRe.Leave += new System.EventHandler(this.txtTrainNameRe_Leave);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(558, 157);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 35;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cbxListPassenger
            // 
            this.cbxListPassenger.CheckOnClick = true;
            this.cbxListPassenger.FormattingEnabled = true;
            this.cbxListPassenger.Location = new System.Drawing.Point(338, 27);
            this.cbxListPassenger.Name = "cbxListPassenger";
            this.cbxListPassenger.Size = new System.Drawing.Size(105, 148);
            this.cbxListPassenger.TabIndex = 36;
            this.cbxListPassenger.SelectedIndexChanged += new System.EventHandler(this.cbxListPassenger_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(338, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 37;
            this.label10.Text = "常用联系人";
            // 
            // cbxCheckCount
            // 
            this.cbxCheckCount.AutoSize = true;
            this.cbxCheckCount.Location = new System.Drawing.Point(616, 129);
            this.cbxCheckCount.Name = "cbxCheckCount";
            this.cbxCheckCount.Size = new System.Drawing.Size(84, 16);
            this.cbxCheckCount.TabIndex = 38;
            this.cbxCheckCount.Text = "检测余票数";
            this.cbxCheckCount.UseVisualStyleBackColor = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "序号";
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "车次";
            this.Column2.HeaderText = "车次";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "发站";
            this.Column3.HeaderText = "发站";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "到站";
            this.Column4.HeaderText = "到站";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "历时";
            this.Column5.HeaderText = "历时";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "商务座";
            this.Column6.HeaderText = "商务座";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            this.Column6.Width = 50;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "特等座";
            this.Column7.HeaderText = "特等座";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            this.Column7.Width = 50;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "一等座";
            this.Column8.HeaderText = "一等座";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Visible = false;
            this.Column8.Width = 50;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "二等座";
            this.Column9.HeaderText = "二等座";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "高级软卧";
            this.Column10.HeaderText = "高级软卧";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            this.Column10.Width = 50;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "软卧";
            this.Column11.HeaderText = "软卧";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Visible = false;
            this.Column11.Width = 50;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "硬卧";
            this.Column12.HeaderText = "硬卧";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Visible = false;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "软座";
            this.Column13.HeaderText = "软座";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            this.Column13.Width = 50;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "硬座";
            this.Column14.HeaderText = "硬座";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "无座";
            this.Column15.HeaderText = "无座";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "其他";
            this.Column16.HeaderText = "其他";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Visible = false;
            this.Column16.Width = 50;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "购票";
            this.Column17.HeaderText = "购票";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Visible = false;
            this.Column17.Width = 50;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "has";
            this.Column18.HeaderText = "预定";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 619);
            this.Controls.Add(this.cbxCheckCount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbxListPassenger);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtTrainNameRe);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblDealy);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtCommitCode);
            this.Controls.Add(this.pcxCommitCode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxSeat);
            this.Controls.Add(this.btnYuding);
            this.Controls.Add(this.dgvQuery);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cbxTo);
            this.Controls.Add(this.cbxFrom);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "12306助手";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcxLoginImgCode)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcxCommitCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLoginImgCode;
        private System.Windows.Forms.PictureBox pcxLoginImgCode;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLoginError;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cbxFrom;
        private System.Windows.Forms.ComboBox cbxTo;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvQuery;
        private System.Windows.Forms.Button btnYuding;
        private System.Windows.Forms.ComboBox cbxSeat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pcxCommitCode;
        private System.Windows.Forms.TextBox txtCommitCode;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label lblDealy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTrainNameRe;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckedListBox cbxListPassenger;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnOpenID;
        private System.Windows.Forms.CheckBox cbxCheckCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
    }
}

