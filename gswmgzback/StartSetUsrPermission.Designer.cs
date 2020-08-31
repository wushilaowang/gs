namespace gswmgzback
{
    partial class StartSetUsrPermission
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
            this.CLB_Districts = new System.Windows.Forms.CheckedListBox();
            this.LB_Usrs = new System.Windows.Forms.ListBox();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.Btn_back = new System.Windows.Forms.Button();
            this.label_UserName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CLB_Districts
            // 
            this.CLB_Districts.FormattingEnabled = true;
            this.CLB_Districts.Location = new System.Drawing.Point(150, 11);
            this.CLB_Districts.MultiColumn = true;
            this.CLB_Districts.Name = "CLB_Districts";
            this.CLB_Districts.Size = new System.Drawing.Size(476, 436);
            this.CLB_Districts.TabIndex = 0;
            this.CLB_Districts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CLB_Districts_MouseClick);
            // 
            // LB_Usrs
            // 
            this.LB_Usrs.FormattingEnabled = true;
            this.LB_Usrs.ItemHeight = 12;
            this.LB_Usrs.Location = new System.Drawing.Point(12, 11);
            this.LB_Usrs.Name = "LB_Usrs";
            this.LB_Usrs.Size = new System.Drawing.Size(132, 436);
            this.LB_Usrs.TabIndex = 1;
            this.LB_Usrs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LB_Usrs_MouseClick);
            // 
            // Btn_Next
            // 
            this.Btn_Next.Location = new System.Drawing.Point(713, 415);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(75, 23);
            this.Btn_Next.TabIndex = 11;
            this.Btn_Next.Text = "下一步";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // Btn_back
            // 
            this.Btn_back.Location = new System.Drawing.Point(632, 415);
            this.Btn_back.Name = "Btn_back";
            this.Btn_back.Size = new System.Drawing.Size(75, 23);
            this.Btn_back.TabIndex = 10;
            this.Btn_back.Text = "上一步";
            this.Btn_back.UseVisualStyleBackColor = true;
            this.Btn_back.Click += new System.EventHandler(this.Btn_back_Click);
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(430, 157);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(41, 12);
            this.label_UserName.TabIndex = 12;
            this.label_UserName.Text = "label1";
            this.label_UserName.Visible = false;
            // 
            // StartSetUsrPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_Next);
            this.Controls.Add(this.Btn_back);
            this.Controls.Add(this.LB_Usrs);
            this.Controls.Add(this.CLB_Districts);
            this.Controls.Add(this.label_UserName);
            this.Name = "StartSetUsrPermission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户分配";
            this.Activated += new System.EventHandler(this.StartSetUsrPermission_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartSetUsrPermission_FormClosed);
            this.Load += new System.EventHandler(this.StartSetUsrPermission_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CLB_Districts;
        private System.Windows.Forms.ListBox LB_Usrs;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Button Btn_back;
        private System.Windows.Forms.Label label_UserName;
    }
}