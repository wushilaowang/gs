namespace gswmgzback
{
    partial class StartSetDistrictCardNumber
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
            this.CB_Districts = new System.Windows.Forms.ComboBox();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.Btn_back = new System.Windows.Forms.Button();
            this.Dgv_CardCount = new System.Windows.Forms.DataGridView();
            this.CardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CardCount)).BeginInit();
            this.SuspendLayout();
            // 
            // CB_Districts
            // 
            this.CB_Districts.FormattingEnabled = true;
            this.CB_Districts.Location = new System.Drawing.Point(3, 4);
            this.CB_Districts.Name = "CB_Districts";
            this.CB_Districts.Size = new System.Drawing.Size(121, 20);
            this.CB_Districts.TabIndex = 0;
            this.CB_Districts.SelectedIndexChanged += new System.EventHandler(this.CB_Districts_SelectedIndexChanged);
            this.CB_Districts.TextUpdate += new System.EventHandler(this.CB_Districts_TextUpdate);
            // 
            // Btn_Next
            // 
            this.Btn_Next.Location = new System.Drawing.Point(715, 415);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(75, 23);
            this.Btn_Next.TabIndex = 7;
            this.Btn_Next.Text = "下一步";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // Btn_back
            // 
            this.Btn_back.Location = new System.Drawing.Point(634, 415);
            this.Btn_back.Name = "Btn_back";
            this.Btn_back.Size = new System.Drawing.Size(75, 23);
            this.Btn_back.TabIndex = 6;
            this.Btn_back.Text = "上一步";
            this.Btn_back.UseVisualStyleBackColor = true;
            this.Btn_back.Click += new System.EventHandler(this.Btn_back_Click);
            // 
            // Dgv_CardCount
            // 
            this.Dgv_CardCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_CardCount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CardName,
            this.CardNumber,
            this.CardCode,
            this.CardItemCount});
            this.Dgv_CardCount.Location = new System.Drawing.Point(130, 4);
            this.Dgv_CardCount.Name = "Dgv_CardCount";
            this.Dgv_CardCount.RowTemplate.Height = 23;
            this.Dgv_CardCount.Size = new System.Drawing.Size(498, 434);
            this.Dgv_CardCount.TabIndex = 8;
            this.Dgv_CardCount.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.Dgv_CardCount_RowStateChanged);
            this.Dgv_CardCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Dgv_CardCount_KeyPress);
            // 
            // CardName
            // 
            this.CardName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CardName.HeaderText = "卡片名称(必填)";
            this.CardName.Name = "CardName";
            this.CardName.ReadOnly = true;
            this.CardName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CardNumber
            // 
            this.CardNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CardNumber.HeaderText = "卡片数量(必填)";
            this.CardNumber.Name = "CardNumber";
            this.CardNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CardCode
            // 
            this.CardCode.HeaderText = "卡片编号";
            this.CardCode.Name = "CardCode";
            this.CardCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CardCode.Visible = false;
            // 
            // CardItemCount
            // 
            this.CardItemCount.HeaderText = "卡片项目数量";
            this.CardItemCount.Name = "CardItemCount";
            this.CardItemCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CardItemCount.Visible = false;
            // 
            // StartSetDistrictCardNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Dgv_CardCount);
            this.Controls.Add(this.Btn_Next);
            this.Controls.Add(this.Btn_back);
            this.Controls.Add(this.CB_Districts);
            this.Name = "StartSetDistrictCardNumber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置卡片数量";
            this.Activated += new System.EventHandler(this.StartSetDistrictCardNumber_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartSetDistrictCardNumber_FormClosed);
            this.Load += new System.EventHandler(this.StartSetDistrictCardNumber_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CardCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_Districts;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Button Btn_back;
        private System.Windows.Forms.DataGridView Dgv_CardCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardItemCount;
    }
}