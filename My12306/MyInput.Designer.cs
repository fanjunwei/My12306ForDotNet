namespace My12306
{
    partial class MyInput
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cbxValue = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 12);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(25, 16);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(173, 21);
            this.txtValue.TabIndex = 1;
            // 
            // cbxValue
            // 
            this.cbxValue.AutoSize = true;
            this.cbxValue.Location = new System.Drawing.Point(4, 19);
            this.cbxValue.Name = "cbxValue";
            this.cbxValue.Size = new System.Drawing.Size(15, 14);
            this.cbxValue.TabIndex = 2;
            this.cbxValue.UseVisualStyleBackColor = true;
            this.cbxValue.Visible = false;
            // 
            // MyInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxValue);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblName);
            this.Name = "MyInput";
            this.Size = new System.Drawing.Size(248, 41);
            this.Load += new System.EventHandler(this.MyInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox cbxValue;
        public System.Windows.Forms.TextBox txtValue;
    }
}
