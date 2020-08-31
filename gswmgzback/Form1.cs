using System;
using System.Windows.Forms;

namespace gswmgzback
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static StartAppraisal startAppraisal;
        public static StartSetCardContent startSetCardContent;
        public static StartSetDistrictCardNumber startSetDistrictCardNumber;
        public static StartSetUser startSetUser;
        public static StartSetUsrPermission startSetUsrPermission;
        public static StartFinish startFinish;
        public static SetUsersPermission changeUserPermission;

        public static CreateQuestionnaire createQuestionnaire;
        public static SetQuestionnaireNumber setQuestionnaireNumber;

        ResultShow resultshow;
        public static string UrlPre ="https://wmcscp.gsinfo.cn/gswmgz/api/";//"http://211.158.66.55/wmgz/api/";//   

        private void 开始测评ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 测评结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            if (resultshow == null)
            {
                resultshow = new ResultShow();
                resultshow.MdiParent = this;

                //去掉边框
                resultshow.FormBorderStyle = FormBorderStyle.None;
                resultshow.Dock = DockStyle.Fill;
                resultshow.WindowState = FormWindowState.Maximized;

                resultshow.Show();

            }

            resultshow.BringToFront();
        }

        private void 创建测评卡片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            startSetUser = null;
            if (startAppraisal == null || startAppraisal.IsDisposed)
            {
                startAppraisal = new StartAppraisal();

                startAppraisal.Show();

            }
            else
            {
                startAppraisal.Show();
            }
        }

        private void 创建调查问卷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            if (createQuestionnaire == null || createQuestionnaire.IsDisposed)
            {
                createQuestionnaire = new CreateQuestionnaire();

                createQuestionnaire.Show();

            }
            else
            {
                createQuestionnaire.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(LogIn.userinfosDatum.Type == 1)
            {
                创建测评ToolStripMenuItem.Visible = false;
                创建测评ToolStripMenuItem.Enabled = false;

                配置用户区域ToolStripMenuItem.Visible = false;
                配置用户区域ToolStripMenuItem.Enabled = false;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 分配用户区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            if (changeUserPermission == null)
            {
                changeUserPermission = new SetUsersPermission();
                changeUserPermission.MdiParent = this;

                //去掉边框
                changeUserPermission.FormBorderStyle = FormBorderStyle.None;
                changeUserPermission.Dock = DockStyle.Fill;
                changeUserPermission.WindowState = FormWindowState.Maximized;

                changeUserPermission.Show();

            }

            changeUserPermission.BringToFront();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            if (startSetUser == null)
            {
                startSetUser = new StartSetUser();
                startSetUser.MdiParent = this;

                //去掉边框
                startSetUser.FormBorderStyle = FormBorderStyle.None;
                startSetUser.Dock = DockStyle.Fill;
                startSetUser.WindowState = FormWindowState.Maximized;

                startSetUser.Show();

            }

            startSetUser.BringToFront();
        }
    }
}
