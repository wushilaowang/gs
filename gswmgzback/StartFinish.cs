using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace gswmgzback
{
    public partial class StartFinish : Form
    {
        public StartFinish()
        {
            InitializeComponent();
        }
        //用于更新progressBar的值及在执行方法的时候，返回方法的处理信息
        private delegate void SetPos(int ipos, string vinfo);


        private void StartFinish_Load(object sender, EventArgs e)
        {
            Thread fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();

        }
        
        private void SetTextMesssage(int ipos, string vinfo)
        {
            if (this.InvokeRequired)
            {
                switch (ipos)
                {
                    case 0:

                        visitApi("StartAppraisal?AppraisalName=" + StartAppraisal.AppraisalType);
                        break;
                    case 1:
                        //生成cardoutlin
                        DataTable DTCL_CardList = StartAppraisal.DT_CardListTable;
                        string CL_cardName = "";
                        string CL_cardCode = "";
                        int CL_cardItemCount = 0;
                        for(int i = 0; i < DTCL_CardList.Rows.Count; i++)
                        {
                            CL_cardName = DTCL_CardList.Rows[i]["cardName"].ToString();
                            CL_cardCode = DTCL_CardList.Rows[i]["cardCode"].ToString();
                            CL_cardItemCount = int.Parse(DTCL_CardList.Rows[i]["cardItemCount"].ToString());
                            visitApi("SetCardList?cardName="+ CL_cardName + "&cardCode="+ CL_cardCode + "&cardItemCount="+ CL_cardItemCount + "&entrance=" + StartAppraisal.AppraisalType);
                        }
                        break;
                    case 2:
                        DataTable DTCC_CardContent = StartSetCardContent.DT_CardContent;
                        
                        string CC_cardCode = "";
                        string CC_item = "";
                        string CC_beizhu = "";
                        string CC_cardName = "";
                        string CC_score = "";

                        for (int i = 0; i < DTCC_CardContent.Rows.Count; i++)
                        {
                            CC_cardCode = DTCC_CardContent.Rows[i]["cardCode"].ToString();
                            CC_item = DTCC_CardContent.Rows[i]["item"].ToString();
                            CC_beizhu = DTCC_CardContent.Rows[i]["beizhu"].ToString();
                            CC_cardName = DTCC_CardContent.Rows[i]["cardName"].ToString();
                            CC_score = DTCC_CardContent.Rows[i]["score"].ToString();
                            visitApi("SetCardContent?cardCode=" + CC_cardCode + "&item=" + CC_item + "&beizhu=" + CC_beizhu+ "&cardName="+ CC_cardName + "&multisign=1" + "&score=" + CC_score + "&entrance=" + StartAppraisal.AppraisalType);
                        }
                        break;
                    case 3:
                        DataTable DTCC_Cardcount = StartSetDistrictCardNumber.DT_Cardcount;
                        string DTCC_districtCode = "";
                        string DTCC_districtName = "";
                        string DTCC_cardCode = "";
                        string DTCC_cardName = "";
                        string DTCC_cardMaxCount = "";
                        string DTCC_cardItemScore = "";
                        for (int i = 0; i < DTCC_Cardcount.Rows.Count; i++)
                        {
                            DTCC_districtCode = DTCC_Cardcount.Rows[i]["DistrictCode"].ToString();
                            DTCC_districtName = DTCC_Cardcount.Rows[i]["DistrictName"].ToString();
                            DTCC_cardCode = DTCC_Cardcount.Rows[i]["CardCode"].ToString();
                            DTCC_cardName = DTCC_Cardcount.Rows[i]["CardName"].ToString();
                            DTCC_cardMaxCount = DTCC_Cardcount.Rows[i]["CardMaxCount"].ToString();
                            DTCC_cardItemScore = DTCC_Cardcount.Rows[i]["CardItemScore"].ToString();
                            visitApi("SetCardCount?districtCode=" + DTCC_districtCode + "&districtName=" + DTCC_districtName + "&cardCode=" + DTCC_cardCode + "&cardName=" + DTCC_cardName + "&cardMaxCount=" + DTCC_cardMaxCount + "&cardScore="+ DTCC_cardItemScore + "&entrance=" + StartAppraisal.AppraisalType);
                        }
                        break;
                    case 4:
                        //DT_UserInfo
                        DataTable DT_UserInfo = StartSetUsrPermission.DT_UserInfo;


                        string Account = "";
                        string Password = "";
                        string RealName = "";
                        string DistrictCode = "";
                        string DistrictName = "";
                        int Type = 0;
                        for (int i = 0; i < DT_UserInfo.Rows.Count; i++)
                        {

                            Account = DT_UserInfo.Rows[i]["Account"].ToString();
                            Password = DT_UserInfo.Rows[i]["Password"].ToString();
                            RealName = DT_UserInfo.Rows[i]["RealName"].ToString();
                            DistrictCode = DT_UserInfo.Rows[i]["DistrictCode"].ToString();
                            DistrictName = DT_UserInfo.Rows[i]["DistrictName"].ToString();
                            Type = int.Parse(DT_UserInfo.Rows[i]["Type"].ToString());
                            visitApi("SetUser?Account=" + Account + "&Password=" + Password + "&RealName=" + RealName + "&DistrictCode=" + DistrictCode + "&DistrictName=" + DistrictName + "&Type=" + Type + "&entrance=" + StartAppraisal.AppraisalType);
                        }
                        break;

                }
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { ipos, vinfo });
            }
            else
            {
                this.label1.Text = "正在生成,当前进度:"+(++ipos) + "/5";
                this.progressBar1.Value = Convert.ToInt32((ipos)*20);
                switch (ipos)
                {
                    case 0:
                        this.textBox1.AppendText("正在生成\r\n");break;
                    case 1:
                        this.textBox1.AppendText("正在生成测评卡片\r\n");
                        break;
                    case 2:
                        this.textBox1.AppendText("正在分配测评卡片\r\n");
                        break;
                    case 3:
                        this.textBox1.AppendText("正在生成用户\r\n");
                        break;
                    case 4:
                        this.textBox1.AppendText("正在分配用户\r\n");
                        this.textBox1.AppendText("完成!!!\r\n");

                        break;
                }

            }
        }

        private void SleepT()
        {
            for (int i = 0; i < 5; i++)
            {
                System.Threading.Thread.Sleep(100);
                SetTextMesssage(i, i.ToString() + "\r\n");
            }
        }

        public static void visitApi(string url)
        {
            url = Form1.UrlPre+url;
            string Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("服务异常");
                return;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartFinish_FormClosed(object sender, FormClosedEventArgs e)
        {
            disposeAll.disposeAllStartForm();
        }
    }

}
