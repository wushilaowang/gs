namespace gswmgzback
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.创建测评ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.创建测评卡片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.创建调查问卷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置用户区域ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看测评结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分配用户区域ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.创建测评ToolStripMenuItem,
            this.配置用户区域ToolStripMenuItem,
            this.查看测评结果ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1384, 37);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 创建测评ToolStripMenuItem
            // 
            this.创建测评ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.创建测评卡片ToolStripMenuItem,
            this.创建调查问卷ToolStripMenuItem});
            this.创建测评ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F);
            this.创建测评ToolStripMenuItem.Name = "创建测评ToolStripMenuItem";
            this.创建测评ToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.创建测评ToolStripMenuItem.Text = "创建测评";
            this.创建测评ToolStripMenuItem.Click += new System.EventHandler(this.开始测评ToolStripMenuItem_Click);
            // 
            // 创建测评卡片ToolStripMenuItem
            // 
            this.创建测评卡片ToolStripMenuItem.Name = "创建测评卡片ToolStripMenuItem";
            this.创建测评卡片ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.创建测评卡片ToolStripMenuItem.Text = "创建测评卡片";
            this.创建测评卡片ToolStripMenuItem.Click += new System.EventHandler(this.创建测评卡片ToolStripMenuItem_Click);
            // 
            // 创建调查问卷ToolStripMenuItem
            // 
            this.创建调查问卷ToolStripMenuItem.Name = "创建调查问卷ToolStripMenuItem";
            this.创建调查问卷ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.创建调查问卷ToolStripMenuItem.Text = "创建调查问卷";
            this.创建调查问卷ToolStripMenuItem.Click += new System.EventHandler(this.创建调查问卷ToolStripMenuItem_Click);
            // 
            // 配置用户区域ToolStripMenuItem
            // 
            this.配置用户区域ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.分配用户区域ToolStripMenuItem,
            this.用户管理ToolStripMenuItem});
            this.配置用户区域ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F);
            this.配置用户区域ToolStripMenuItem.Name = "配置用户区域ToolStripMenuItem";
            this.配置用户区域ToolStripMenuItem.Size = new System.Drawing.Size(104, 31);
            this.配置用户区域ToolStripMenuItem.Text = "用户管理";
            // 
            // 查看测评结果ToolStripMenuItem
            // 
            this.查看测评结果ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F);
            this.查看测评结果ToolStripMenuItem.Name = "查看测评结果ToolStripMenuItem";
            this.查看测评结果ToolStripMenuItem.Size = new System.Drawing.Size(144, 31);
            this.查看测评结果ToolStripMenuItem.Text = "测评结果查看";
            this.查看测评结果ToolStripMenuItem.Click += new System.EventHandler(this.测评结果ToolStripMenuItem_Click);
            // 
            // 分配用户区域ToolStripMenuItem
            // 
            this.分配用户区域ToolStripMenuItem.Name = "分配用户区域ToolStripMenuItem";
            this.分配用户区域ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.分配用户区域ToolStripMenuItem.Text = "分配用户区域";
            this.分配用户区域ToolStripMenuItem.Click += new System.EventHandler(this.分配用户区域ToolStripMenuItem_Click);
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(204, 32);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            this.用户管理ToolStripMenuItem.Click += new System.EventHandler(this.用户管理ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 791);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "测评管理系统6.X";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 创建测评ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看测评结果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置用户区域ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 创建测评卡片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 创建调查问卷ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分配用户区域ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem;
    }
}

