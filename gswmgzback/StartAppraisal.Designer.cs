namespace gswmgzback
{
    partial class StartAppraisal
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_AppraisalType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.B = new System.Windows.Forms.Button();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.DG_CardList = new System.Windows.Forms.DataGridView();
            this.cardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_SumScore = new System.Windows.Forms.TextBox();
            this.LB_SumScore = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_CardList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.CB_AppraisalType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.B);
            this.panel1.Controls.Add(this.Btn_Next);
            this.panel1.Controls.Add(this.DG_CardList);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TB_SumScore);
            this.panel1.Controls.Add(this.LB_SumScore);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 15F);
            this.button1.Location = new System.Drawing.Point(611, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 35);
            this.button1.TabIndex = 17;
            this.button1.Text = "导入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F);
            this.label5.Location = new System.Drawing.Point(728, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "(必填)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(593, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 15;
            // 
            // CB_AppraisalType
            // 
            this.CB_AppraisalType.Font = new System.Drawing.Font("宋体", 15F);
            this.CB_AppraisalType.FormattingEnabled = true;
            this.CB_AppraisalType.Items.AddRange(new object[] {
            "文明村镇",
            "文明校园",
            "少年宫",
            "文明单位",
            "文明社区",
            "未成年人思想道德建设"});
            this.CB_AppraisalType.Location = new System.Drawing.Point(535, 8);
            this.CB_AppraisalType.Name = "CB_AppraisalType";
            this.CB_AppraisalType.Size = new System.Drawing.Size(190, 28);
            this.CB_AppraisalType.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(360, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "选择本次测评类型";
            // 
            // B
            // 
            this.B.Font = new System.Drawing.Font("宋体", 15F);
            this.B.Location = new System.Drawing.Point(611, 412);
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(90, 35);
            this.B.TabIndex = 12;
            this.B.Text = "删除";
            this.B.UseVisualStyleBackColor = true;
            this.B.Click += new System.EventHandler(this.B_Click);
            // 
            // Btn_Next
            // 
            this.Btn_Next.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_Next.Location = new System.Drawing.Point(707, 412);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(90, 35);
            this.Btn_Next.TabIndex = 11;
            this.Btn_Next.Text = "下一步";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // DG_CardList
            // 
            this.DG_CardList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_CardList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cardName,
            this.cardItemCount});
            this.DG_CardList.Location = new System.Drawing.Point(205, 59);
            this.DG_CardList.Name = "DG_CardList";
            this.DG_CardList.RowTemplate.Height = 23;
            this.DG_CardList.Size = new System.Drawing.Size(381, 379);
            this.DG_CardList.TabIndex = 10;
            this.DG_CardList.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.DG_CardList_RowStateChanged);
            this.DG_CardList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DG_CardList_KeyDown);
            this.DG_CardList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DG_CardList_KeyPress);
            // 
            // cardName
            // 
            this.cardName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cardName.HeaderText = "卡片名称(必填)";
            this.cardName.Name = "cardName";
            // 
            // cardItemCount
            // 
            this.cardItemCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cardItemCount.HeaderText = "卡片内选项数量(必填)";
            this.cardItemCount.Name = "cardItemCount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(29, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "设定本次测评卡片";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(256, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "分(必填)";
            // 
            // TB_SumScore
            // 
            this.TB_SumScore.Font = new System.Drawing.Font("宋体", 15F);
            this.TB_SumScore.Location = new System.Drawing.Point(204, 6);
            this.TB_SumScore.Name = "TB_SumScore";
            this.TB_SumScore.Size = new System.Drawing.Size(46, 30);
            this.TB_SumScore.TabIndex = 7;
            this.TB_SumScore.Text = "0";
            // 
            // LB_SumScore
            // 
            this.LB_SumScore.AutoSize = true;
            this.LB_SumScore.Font = new System.Drawing.Font("宋体", 15F);
            this.LB_SumScore.Location = new System.Drawing.Point(29, 9);
            this.LB_SumScore.Name = "LB_SumScore";
            this.LB_SumScore.Size = new System.Drawing.Size(169, 20);
            this.LB_SumScore.TabIndex = 6;
            this.LB_SumScore.Text = "设定本次卡片总分";
            // 
            // StartAppraisal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "StartAppraisal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "总设定";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartAppraisal_FormClosed);
            this.Load += new System.EventHandler(this.StartAppraisal_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_CardList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.DataGridView DG_CardList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_SumScore;
        private System.Windows.Forms.Label LB_SumScore;
        private System.Windows.Forms.Button B;
        private System.Windows.Forms.ComboBox CB_AppraisalType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardItemCount;
    }
}