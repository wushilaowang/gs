using System;
using System.Data;
using System.Windows.Forms;

namespace gswmgzback
{
    public partial class CreateQuestionnaire : Form
    {
        public CreateQuestionnaire()
        {
            InitializeComponent();
        }

        //卡片详细内容表 填入CardContent 里
        public static string Entrance = "";
        public static DataTable DT_QuestionnaireContent;
        public static int questionnaireItemCount = 0;
        public static int questionnaireSumScore = 0;
        //记录总卡片存入cardoutlin
        public static DataTable DT_CardListTable = new DataTable();

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            if (TB_SumScore.Text == "0")
            {
                MessageBox.Show("总分为0");
                return;
            }
            if (Combox_Type.Text == "")
            {
                MessageBox.Show("尚未选择测评类型");
                return;
            }

            Entrance = Combox_Type.Text;
            

            DT_QuestionnaireContent.Rows.Clear();
            questionnaireItemCount = 0;

            for (int i = 0; i < Dgv_questionnaireContent.Rows.Count; i++)
            {
                if (Dgv_questionnaireContent.Rows[i].Cells["CardContent"].Value!= null)
                {
                    DataRow row = DT_QuestionnaireContent.NewRow();
                    row["cardCode"] = "K9001";
                    row["item"] = Dgv_questionnaireContent.Rows[i].Cells["CardContent"].Value.ToString();
                    row["beizhu"] = Dgv_questionnaireContent.Rows[i].Cells["Beizhu"].Value == null ? "" : Dgv_questionnaireContent.Rows[i].Cells["Beizhu"].Value.ToString();
                    row["cardName"] = "调查问卷";
                    row["score"] = Dgv_questionnaireContent.Rows[i].Cells["Score"].Value==null ? 0 : double.Parse(Dgv_questionnaireContent.Rows[i].Cells["Score"].Value.ToString());
                   
                    DT_QuestionnaireContent.Rows.Add(row);
                    questionnaireItemCount++;
                }
            }


            DataRow dr = DT_CardListTable.NewRow();
            dr["cardCode"] = "K9001";
            dr["cardName"] = "调查问卷";
            dr["cardItemCount"] = questionnaireItemCount;

            DT_CardListTable.Rows.Add(dr);

            questionnaireSumScore = int.Parse(TB_SumScore.Text.ToString());
            //显示下一页
            this.Hide();
            if (Form1.setQuestionnaireNumber == null || Form1.setQuestionnaireNumber.IsDisposed)
            {
                Form1.setQuestionnaireNumber = new SetQuestionnaireNumber();

                Form1.setQuestionnaireNumber.Show();

            }
            else
            {
                Form1.setQuestionnaireNumber.Show();
            }
             
        }

        private void B_Click(object sender, EventArgs e)
        {

        }

        private void CreateQuestionnaire_Load(object sender, EventArgs e)
        {

            DT_QuestionnaireContent = new DataTable();
            DT_QuestionnaireContent.Columns.Add("cardCode", typeof(string));
            DT_QuestionnaireContent.Columns.Add("item", typeof(string));
            DT_QuestionnaireContent.Columns.Add("beizhu", typeof(string));//beizhu
            DT_QuestionnaireContent.Columns.Add("cardName", typeof(string));
            DT_QuestionnaireContent.Columns.Add("score", typeof(double));

            DT_CardListTable.Columns.Add("cardCode", typeof(string));
            DT_CardListTable.Columns.Add("cardName", typeof(string));
            DT_CardListTable.Columns.Add("cardItemCount", typeof(int));
        }
    }
}
