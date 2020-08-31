namespace gswmgzback
{
    partial class StartSetUser
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
            this.components = new System.ComponentModel.Container();
            this.Dgv_SimpleUser = new System.Windows.Forms.DataGridView();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.Btn_back = new System.Windows.Forms.Button();
            this.usrTypeEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usrTypeEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.BTN_OK = new System.Windows.Forms.Button();
            this.Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_SimpleUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usrTypeEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usrTypeEBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_SimpleUser
            // 
            this.Dgv_SimpleUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_SimpleUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Account,
            this.PassWord,
            this.RealName,
            this.UserType});
            this.Dgv_SimpleUser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Dgv_SimpleUser.Location = new System.Drawing.Point(3, 2);
            this.Dgv_SimpleUser.Name = "Dgv_SimpleUser";
            this.Dgv_SimpleUser.RowTemplate.Height = 23;
            this.Dgv_SimpleUser.Size = new System.Drawing.Size(623, 436);
            this.Dgv_SimpleUser.TabIndex = 0;
            this.Dgv_SimpleUser.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Dgv_SimpleUser_CellFormatting);
            this.Dgv_SimpleUser.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Dgv_SimpleUser_EditingControlShowing);
            // 
            // Btn_Next
            // 
            this.Btn_Next.Location = new System.Drawing.Point(713, 415);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(75, 23);
            this.Btn_Next.TabIndex = 9;
            this.Btn_Next.Text = "下一步";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // Btn_back
            // 
            this.Btn_back.Location = new System.Drawing.Point(632, 415);
            this.Btn_back.Name = "Btn_back";
            this.Btn_back.Size = new System.Drawing.Size(75, 23);
            this.Btn_back.TabIndex = 8;
            this.Btn_back.Text = "上一步";
            this.Btn_back.UseVisualStyleBackColor = true;
            this.Btn_back.Click += new System.EventHandler(this.Btn_back_Click);
            // 
            // usrTypeEBindingSource
            // 
            this.usrTypeEBindingSource.DataSource = typeof(gswmgzback.UserTypeEnum.UsrTypeE);
            // 
            // usrTypeEBindingSource1
            // 
            this.usrTypeEBindingSource1.DataSource = typeof(gswmgzback.UserTypeEnum.UsrTypeE);
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.Location = new System.Drawing.Point(632, 386);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.Btn_Delete.TabIndex = 10;
            this.Btn_Delete.Text = "删除";
            this.Btn_Delete.UseVisualStyleBackColor = true;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // BTN_OK
            // 
            this.BTN_OK.Location = new System.Drawing.Point(713, 386);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new System.Drawing.Size(75, 23);
            this.BTN_OK.TabIndex = 11;
            this.BTN_OK.Text = "完成";
            this.BTN_OK.UseVisualStyleBackColor = true;
            this.BTN_OK.Click += new System.EventHandler(this.BTN_OK_Click);
            // 
            // Account
            // 
            this.Account.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Account.FillWeight = 1F;
            this.Account.HeaderText = "账号(必填)";
            this.Account.Name = "Account";
            // 
            // PassWord
            // 
            this.PassWord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PassWord.FillWeight = 1F;
            this.PassWord.HeaderText = "密码(必填)";
            this.PassWord.Name = "PassWord";
            // 
            // RealName
            // 
            this.RealName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RealName.FillWeight = 1F;
            this.RealName.HeaderText = "姓名(必填)";
            this.RealName.Name = "RealName";
            // 
            // UserType
            // 
            this.UserType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UserType.FillWeight = 1F;
            this.UserType.HeaderText = "账号类型(必选)";
            this.UserType.Items.AddRange(new object[] {
            "系统管理员",
            "测评带队",
            "测评成员"});
            this.UserType.Name = "UserType";
            this.UserType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UserType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // StartSetUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BTN_OK);
            this.Controls.Add(this.Btn_Delete);
            this.Controls.Add(this.Btn_Next);
            this.Controls.Add(this.Btn_back);
            this.Controls.Add(this.Dgv_SimpleUser);
            this.Name = "StartSetUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置用户";
            this.Activated += new System.EventHandler(this.StartSetUser_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartSetUser_FormClosed);
            this.Load += new System.EventHandler(this.StartSetUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_SimpleUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usrTypeEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usrTypeEBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgv_SimpleUser;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Button Btn_back;
        private System.Windows.Forms.BindingSource usrTypeEBindingSource;
        private System.Windows.Forms.BindingSource usrTypeEBindingSource1;
        private System.Windows.Forms.Button Btn_Delete;
        private System.Windows.Forms.Button BTN_OK;
        private System.Windows.Forms.DataGridViewTextBoxColumn Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn PassWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealName;
        private System.Windows.Forms.DataGridViewComboBoxColumn UserType;
    }
}