using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static gswmgzback.AlldistrictContent;

namespace gswmgzback
{
    public partial class SetQuestionnaireNumber : Form
    {


        //保存县/区详细信息,包括code,name,id等等
        public static List<AlldistrictDatum> LAlldistricts = new List<AlldistrictDatum>();
        string Cur_DistrictName = null;

        //储存卡片在每个地区的数量
        public static DataTable DT_Cardcount;
        DataTable QuestionnaireCardName = CreateQuestionnaire.DT_CardListTable;
        DataTable QuestionnaireContent = CreateQuestionnaire.DT_QuestionnaireContent;

        public SetQuestionnaireNumber()
        {
            InitializeComponent();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            AddCardCount();

            //上传数据
            //1.增加卡片总览
            foreach(DataRow item in QuestionnaireCardName.Rows)
            {
                visitApi("SetCardList?cardName=" + item["cardName"] + "&cardCode=" + item["cardCode"] + "&cardItemCount=" + item["cardItemCount"] + "&entrance=" + CreateQuestionnaire.Entrance);
            }

            //2.增加卡片内容
            foreach (DataRow item in QuestionnaireContent.Rows)
            {
                visitApi("SetCardContent?cardCode=" + item["cardCode"] + "&item=" + item["item"] + "&beizhu=" + item["beizhu"] + "&cardName=" + item["cardName"]+"&score="+item["score"] + "&entrance=" + CreateQuestionnaire.Entrance);
            }
            //3.对应地区增加卡片数量与分数

            foreach(DataRow item in DT_Cardcount.Rows)
            {
                visitApi("SetCardCount?districtCode=" + item["DistrictCode"] + "&districtName=" + item["DistrictName"] + "&cardCode=" + item["CardCode"] + "&cardName=" + item["CardName"] + "&cardMaxCount=" + item["CardMaxCount"] + "&cardScore=" + item["CardItemScore"] + "&entrance=" + CreateQuestionnaire.Entrance);
            }
            MessageBox.Show("完成!");
            this.Close();

        }
        //访问API
        public void visitApi(string url)
        {
            url = Form1.UrlPre + url;
            string Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("服务异常");
                return;
            }

        }
        private void SetQuestionnaireNumber_Load(object sender, EventArgs e)
        {

            DT_Cardcount = new DataTable();
            DT_Cardcount.Columns.Add("DistrictCode", typeof(string));
            DT_Cardcount.Columns.Add("DistrictName", typeof(string));
            DT_Cardcount.Columns.Add("CardCode", typeof(string));
            DT_Cardcount.Columns.Add("CardName", typeof(string));
            DT_Cardcount.Columns.Add("CardMaxCount", typeof(int));
            DT_Cardcount.Columns.Add("CardItemScore", typeof(double));

            if (Cur_DistrictName == CB_Districts.Text)
            {
                return;
            }

            //获取所有地区
            CB_Districts.Items.Clear();
            string url = Form1.UrlPre + "ShowAlldistrict";
            string Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("网络未连接");
                return;
            }
            //将字符串转换成json
            var Httpjsonresult = JObject.Parse(Httpres);
            //获取json中的data部分
            JToken HttpJsonvalue = Httpjsonresult.GetValue("data");
            int resultrow = HttpJsonvalue.Count();

            for (int i = 0; i < resultrow; i++)
            {
                int column = HttpJsonvalue[i].Count();
                var s = HttpJsonvalue[i].ToString();
                AlldistrictDatum alldistrictdatum = JsonConvert.DeserializeObject<AlldistrictDatum>(s);
                CB_Districts.Items.Add(alldistrictdatum.districtName);
                LAlldistricts.Add(alldistrictdatum);
            }
        }

        private void CB_Districts_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            AddCardCount();
            findOrUpdate();

        }
        //在已有数据中查询,如果有结果就把数据填进去,否则初始化新数据
        private void findOrUpdate()
        {
            if (CB_Districts.Text == "")
            {
                return;
            }
            string SelectDistrictName = CB_Districts.Text;
            Cur_DistrictName = CB_Districts.Text;
            Dgv_CardCount.Rows.Clear();

            for (int i = 0; i < QuestionnaireCardName.Rows.Count; i++)
            {

                int j = 0;
                for (j = 0; j < DT_Cardcount.Rows.Count; j++)
                {
                    //先找地方
                    if (DT_Cardcount.Rows[j]["DistrictName"].ToString().Trim() == SelectDistrictName)
                    {
                        //再看具体的卡片//使用不变的cardcode,以防万一
                        if (DT_Cardcount.Rows[j]["cardCode"].ToString().Trim() == QuestionnaireCardName.Rows[i]["cardCode"].ToString().Trim())
                        {
                            Dgv_CardCount.Rows.Add(QuestionnaireCardName.Rows[i]["CardName"], DT_Cardcount.Rows[j]["CardMaxCount"], QuestionnaireCardName.Rows[i]["CardCode"], QuestionnaireCardName.Rows[i]["cardItemCount"]);
                            break;
                        }
                    }
                }
                //如果找不到,就加初始
                if (j == DT_Cardcount.Rows.Count)
                {
                    Dgv_CardCount.Rows.Add(QuestionnaireCardName.Rows[i]["cardName"].ToString().Trim(), 0, QuestionnaireCardName.Rows[i]["cardCode"].ToString().Trim(), QuestionnaireCardName.Rows[i]["cardItemCount"].ToString().Trim(),"地级");
                }

            }
        }
        //保存当前编辑的县
        private void AddCardCount()
        {
            int CardScore = CreateQuestionnaire.questionnaireSumScore;
            //把焦点清空可以保存当前行
            Dgv_CardCount.CurrentCell = null;
            if (Cur_DistrictName == null)
            {
                return;
            }
            string SDistrictCode = "";
            string SDistrictName = "";
            //获取地址信息
            for (int l = 0; l < LAlldistricts.Count; l++)
            {
                if (LAlldistricts[l].districtName == Cur_DistrictName)
                {

                    SDistrictCode = LAlldistricts[l].districtCode;
                    SDistrictName = LAlldistricts[l].districtName;
                }
            }

            //一行一行查询是否存在,存在就修改,不存在就保存

            for (int i = 0; i < Dgv_CardCount.Rows.Count; i++)
            {
                int k = 0;
                for (k = 0; k < DT_Cardcount.Rows.Count; k++)
                {
                    //先判断地方
                    if (DT_Cardcount.Rows[k]["DistrictName"].ToString().Trim() == Cur_DistrictName)
                    {
                        //在判断卡片
                        if (DT_Cardcount.Rows[k]["cardCode"].ToString().Trim() == Dgv_CardCount.Rows[i].Cells["cardCode"].Value.ToString().Trim())
                        {
                            DT_Cardcount.Rows[k]["CardMaxCount"] = Dgv_CardCount.Rows[i].Cells["CardNumber"].Value;
                            DT_Cardcount.Rows[k]["CardName"] = Dgv_CardCount.Rows[i].Cells["CardName"].Value;

                            DT_Cardcount.Rows[k]["CardItemScore"] = 0;// CardScore / (double.Parse(Dgv_CardCount.Rows[i].Cells["CardItemCount"].Value.ToString()) * double.Parse(Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString()));


                            break;
                        }
                    }
                }
                //如果没找到
                if (k == DT_Cardcount.Rows.Count)
                {
                    //获取卡片信息
                    string SCardCode = Dgv_CardCount.Rows[i].Cells["CardCode"].Value.ToString().Trim();
                    string SCardName = Dgv_CardCount.Rows[i].Cells["CardName"].Value.ToString().Trim();
                    int SCardMaxCount = 0;
                    //如果有异常就提示
                    if (!int.TryParse(Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString().Trim(), out SCardMaxCount))
                    {
                        MessageBox.Show("卡片数量必须是整数,请检查后重试");
                    }
                    double SCardItemScore = 0;// CardScore / (double.Parse(Dgv_CardCount.Rows[i].Cells["CardItemCount"].Value.ToString()) * double.Parse(Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString()));


                    DT_Cardcount.Rows.Add(SDistrictCode, SDistrictName, SCardCode, SCardName, SCardMaxCount, SCardItemScore);
                }
            }
        }
    }
}
