namespace R12306
{
    partial class OpenIE
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpenIE = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(91, 105);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnOpenIE
            // 
            this.btnOpenIE.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOpenIE.ForeColor = System.Drawing.Color.Red;
            this.btnOpenIE.Location = new System.Drawing.Point(190, 105);
            this.btnOpenIE.Name = "btnOpenIE";
            this.btnOpenIE.Size = new System.Drawing.Size(75, 23);
            this.btnOpenIE.TabIndex = 1;
            this.btnOpenIE.Text = "打开IE";
            this.btnOpenIE.UseVisualStyleBackColor = true;
            this.btnOpenIE.Click += new System.EventHandler(this.btnOpenIE_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.Location = new System.Drawing.Point(0, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(345, 79);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OpenIE
            // 
            this.AcceptButton = this.btnOpenIE;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 140);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnOpenIE);
            this.Controls.Add(this.btnClose);
            this.Name = "OpenIE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "成功";
            this.Load += new System.EventHandler(this.OpenIE_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpenIE;
        private System.Windows.Forms.Label lblMessage;
    }
}