namespace gswmgzback
{
    partial class CreateQuestionnaire
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
            this.label1 = new System.Windows.Forms.Label();
            this.TB_SumScore = new System.Windows.Forms.TextBox();
            this.LB_SumScore = new System.Windows.Forms.Label();
            this.Dgv_questionnaireContent = new System.Windows.Forms.DataGridView();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.B = new System.Windows.Forms.Button();
            this.Combox_Type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CardContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Beizhu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_questionnaireContent)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(239, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "分";
            // 
            // TB_SumScore
            // 
            this.TB_SumScore.Font = new System.Drawing.Font("宋体", 15F);
            this.TB_SumScore.Location = new System.Drawing.Point(187, 6);
            this.TB_SumScore.Name = "TB_SumScore";
            this.TB_SumScore.Size = new System.Drawing.Size(46, 30);
            this.TB_SumScore.TabIndex = 10;
            this.TB_SumScore.Text = "0";
            // 
            // LB_SumScore
            // 
            this.LB_SumScore.AutoSize = true;
            this.LB_SumScore.Font = new System.Drawing.Font("宋体", 15F);
            this.LB_SumScore.Location = new System.Drawing.Point(12, 9);
            this.LB_SumScore.Name = "LB_SumScore";
            this.LB_SumScore.Size = new System.Drawing.Size(169, 20);
            this.LB_SumScore.TabIndex = 9;
            this.LB_SumScore.Text = "设定本次问卷总分";
            // 
            // Dgv_questionnaireContent
            // 
            this.Dgv_questionnaireContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_questionnaireContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CardContent,
            this.Beizhu,
            this.Score});
            this.Dgv_questionnaireContent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Dgv_questionnaireContent.Location = new System.Drawing.Point(12, 42);
            this.Dgv_questionnaireContent.Name = "Dgv_questionnaireContent";
            this.Dgv_questionnaireContent.RowTemplate.Height = 23;
            this.Dgv_questionnaireContent.Size = new System.Drawing.Size(584, 396);
            this.Dgv_questionnaireContent.TabIndex = 12;
            // 
            // Btn_Next
            // 
            this.Btn_Next.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_Next.Location = new System.Drawing.Point(698, 403);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(90, 35);
            this.Btn_Next.TabIndex = 14;
            this.Btn_Next.Text = "下一步";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // B
            // 
            this.B.Font = new System.Drawing.Font("宋体", 15F);
            this.B.Location = new System.Drawing.Point(602, 403);
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(90, 35);
            this.B.TabIndex = 15;
            this.B.Text = "删除";
            this.B.UseVisualStyleBackColor = true;
            this.B.Click += new System.EventHandler(this.B_Click);
            // 
            // Combox_Type
            // 
            this.Combox_Type.Font = new System.Drawing.Font("宋体", 15F);
            this.Combox_Type.FormattingEnabled = true;
            this.Combox_Type.Items.AddRange(new object[] {
            "文明单位",
            "文明村镇",
            "文明社区",
            "文明校园",
            "少年宫",
            "未成年人"});
            this.Combox_Type.Location = new System.Drawing.Point(668, 8);
            this.Combox_Type.Name = "Combox_Type";
            this.Combox_Type.Size = new System.Drawing.Size(121, 28);
            this.Combox_Type.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(523, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "选择测评类型:";
            // 
            // CardContent
            // 
            this.CardContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CardContent.HeaderText = "问卷选项(必填)";
            this.CardContent.Name = "CardContent";
            this.CardContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Beizhu
            // 
            this.Beizhu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Beizhu.HeaderText = "选项备注(选填)";
            this.Beizhu.Name = "Beizhu";
            this.Beizhu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Score
            // 
            this.Score.HeaderText = "分值";
            this.Score.Name = "Score";
            // 
            // CreateQuestionnaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Combox_Type);
            this.Controls.Add(this.B);
            this.Controls.Add(this.Btn_Next);
            this.Controls.Add(this.Dgv_questionnaireContent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_SumScore);
            this.Controls.Add(this.LB_SumScore);
            this.Name = "CreateQuestionnaire";
            this.Text = "创建调查问卷";
            this.Load += new System.EventHandler(this.CreateQuestionnaire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_questionnaireContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_SumScore;
        private System.Windows.Forms.Label LB_SumScore;
        private System.Windows.Forms.DataGridView Dgv_questionnaireContent;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Button B;
        private System.Windows.Forms.ComboBox Combox_Type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Beizhu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
    }
}