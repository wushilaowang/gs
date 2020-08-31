namespace gswmgzback
{
    partial class SetUsersPermission
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgv_user = new System.Windows.Forms.DataGridView();
            this.Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsrPermission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_user)).BeginInit();
            this.SuspendLayout();
            // 
            // CLB_Districts
            // 
            this.CLB_Districts.FormattingEnabled = true;
            this.CLB_Districts.Location = new System.Drawing.Point(316, 34);
            this.CLB_Districts.Margin = new System.Windows.Forms.Padding(5);
            this.CLB_Districts.MultiColumn = true;
            this.CLB_Districts.Name = "CLB_Districts";
            this.CLB_Districts.Size = new System.Drawing.Size(1003, 654);
            this.CLB_Districts.TabIndex = 2;
            this.CLB_Districts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CLB_Districts_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(114, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(706, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "区域列表";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(316, 696);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 41);
            this.button1.TabIndex = 6;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgv_user
            // 
            this.dgv_user.AllowUserToAddRows = false;
            this.dgv_user.AllowUserToDeleteRows = false;
            this.dgv_user.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_user.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Account,
            this.UsrName,
            this.UsrPermission});
            this.dgv_user.Location = new System.Drawing.Point(12, 34);
            this.dgv_user.Name = "dgv_user";
            this.dgv_user.ReadOnly = true;
            this.dgv_user.RowHeadersVisible = false;
            this.dgv_user.RowTemplate.Height = 23;
            this.dgv_user.Size = new System.Drawing.Size(296, 704);
            this.dgv_user.TabIndex = 7;
            this.dgv_user.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_user_CellClick);
            this.dgv_user.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_user_MouseClick);
            // 
            // Account
            // 
            this.Account.HeaderText = "账号";
            this.Account.Name = "Account";
            this.Account.ReadOnly = true;
            // 
            // UsrName
            // 
            this.UsrName.HeaderText = "名称";
            this.UsrName.Name = "UsrName";
            this.UsrName.ReadOnly = true;
            // 
            // UsrPermission
            // 
            this.UsrPermission.HeaderText = "权限";
            this.UsrPermission.Name = "UsrPermission";
            this.UsrPermission.ReadOnly = true;
            // 
            // SetUsersPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 750);
            this.Controls.Add(this.dgv_user);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CLB_Districts);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SetUsersPermission";
            this.Text = "用户区域分配";
            this.Activated += new System.EventHandler(this.SetUsersPermission_Activated);
            this.Load += new System.EventHandler(this.ChangeUserPermission_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_user)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckedListBox CLB_Districts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsrName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsrPermission;
    }
}