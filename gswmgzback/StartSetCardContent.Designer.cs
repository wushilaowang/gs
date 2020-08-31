namespace gswmgzback
{
    partial class StartSetCardContent
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
            this.Dgv_CardContent = new System.Windows.Forms.DataGridView();
            this.Label_CardName = new System.Windows.Forms.Label();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Btn_back = new System.Windows.Forms.Button();
            this.LB_CardNames = new System.Windows.Forms.ListBox();
            this.CardContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Beizhu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CardContent)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Dgv_CardContent);
            this.panel1.Controls.Add(this.Label_CardName);
            this.panel1.Controls.Add(this.Btn_Next);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Btn_back);
            this.panel1.Controls.Add(this.LB_CardNames);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 3;
            // 
            // Dgv_CardContent
            // 
            this.Dgv_CardContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_CardContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CardContent,
            this.Beizhu,
            this.score});
            this.Dgv_CardContent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Dgv_CardContent.Location = new System.Drawing.Point(154, 27);
            this.Dgv_CardContent.Name = "Dgv_CardContent";
            this.Dgv_CardContent.RowTemplate.Height = 23;
            this.Dgv_CardContent.Size = new System.Drawing.Size(643, 382);
            this.Dgv_CardContent.TabIndex = 7;
            this.Dgv_CardContent.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.Dgv_CardContent_RowStateChanged);
            this.Dgv_CardContent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Dgv_CardContent_KeyPress);
            // 
            // Label_CardName
            // 
            this.Label_CardName.AutoSize = true;
            this.Label_CardName.Font = new System.Drawing.Font("宋体", 15F);
            this.Label_CardName.Location = new System.Drawing.Point(423, 4);
            this.Label_CardName.Name = "Label_CardName";
            this.Label_CardName.Size = new System.Drawing.Size(69, 20);
            this.Label_CardName.TabIndex = 6;
            this.Label_CardName.Text = "卡片名";
            // 
            // Btn_Next
            // 
            this.Btn_Next.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_Next.Location = new System.Drawing.Point(707, 411);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(90, 35);
            this.Btn_Next.TabIndex = 5;
            this.Btn_Next.Text = "下一步";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 15F);
            this.button1.Location = new System.Drawing.Point(515, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "导入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Btn_back
            // 
            this.Btn_back.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_back.Location = new System.Drawing.Point(611, 411);
            this.Btn_back.Name = "Btn_back";
            this.Btn_back.Size = new System.Drawing.Size(90, 35);
            this.Btn_back.TabIndex = 4;
            this.Btn_back.Text = "上一步";
            this.Btn_back.UseVisualStyleBackColor = true;
            this.Btn_back.Click += new System.EventHandler(this.Btn_back_Click);
            // 
            // LB_CardNames
            // 
            this.LB_CardNames.Font = new System.Drawing.Font("宋体", 15F);
            this.LB_CardNames.FormattingEnabled = true;
            this.LB_CardNames.HorizontalScrollbar = true;
            this.LB_CardNames.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.LB_CardNames.ItemHeight = 20;
            this.LB_CardNames.Location = new System.Drawing.Point(12, 12);
            this.LB_CardNames.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.LB_CardNames.Name = "LB_CardNames";
            this.LB_CardNames.Size = new System.Drawing.Size(136, 424);
            this.LB_CardNames.TabIndex = 3;
            this.LB_CardNames.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LB_CardNames_MouseClick);
            this.LB_CardNames.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LB_CardNames_DrawItem);
            this.LB_CardNames.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.LB_CardNames_MeasureItem);
            // 
            // CardContent
            // 
            this.CardContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CardContent.HeaderText = "卡片选项(必填)";
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
            // score
            // 
            this.score.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.score.HeaderText = "分值(必填)";
            this.score.Name = "score";
            this.score.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StartSetCardContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "StartSetCardContent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置卡片内容";
            this.Activated += new System.EventHandler(this.StartSetCardContent_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartSetCardContent_FormClosed);
            this.Load += new System.EventHandler(this.StartSetCardContent_Load);
            this.Shown += new System.EventHandler(this.StartSetCardContent_Shown);
            this.Enter += new System.EventHandler(this.StartSetCardContent_Enter);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CardContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Button Btn_back;
        private System.Windows.Forms.ListBox LB_CardNames;
        private System.Windows.Forms.Label Label_CardName;
        private System.Windows.Forms.DataGridView Dgv_CardContent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Beizhu;
        private System.Windows.Forms.DataGridViewTextBoxColumn score;
    }
}