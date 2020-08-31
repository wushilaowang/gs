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
    public partial class StartSetDistrictCardNumber : Form
    {
        public StartSetDistrictCardNumber()
        {
            InitializeComponent();
        }

        //储存卡片在每个地区的数量
        public static DataTable DT_Cardcount;

        ////储存每个地区的卡片项目总量
        //public static DataTable DT_itemcount;

        //记录当前正在输入的县的名字
        string Cur_DistrictName;
        //获取所有卡片
        DataTable AllCardName = StartAppraisal.DT_CardListTable;

        public double CardScore = 0.0;
        
        //保存县/区详细信息,包括code,name,id等等
        public static  List<AlldistrictDatum> Ldistricts = new List<AlldistrictDatum>();

        private void StartSetDistrictCardNumber_Load(object sender, EventArgs e)
        {
            ////DataGridView无默认行
            Dgv_CardCount.AllowUserToAddRows = false;


            DT_Cardcount = new DataTable();
            DT_Cardcount.Columns.Add("DistrictCode", typeof(string));
            DT_Cardcount.Columns.Add("DistrictName", typeof(string));
            DT_Cardcount.Columns.Add("CardCode", typeof(string));
            DT_Cardcount.Columns.Add("CardName", typeof(string));
            DT_Cardcount.Columns.Add("CardMaxCount", typeof(int));
            DT_Cardcount.Columns.Add("CardItemScore", typeof(double));
            
        }

        private void iniStartSetDistrictCardNumber()
        {
            if (Cur_DistrictName == CB_Districts.Text)
            {
                return;
            }

            //获取所有地区
            CB_Districts.Items.Clear();
            Ldistricts.Clear();
            string url = Form1.UrlPre+"ShowAlldistrict";
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
                Ldistricts.Add(alldistrictdatum);
            }

            Dgv_CardCount.Rows.Clear();


            findOrUpdate();

        }

        private void CB_Districts_TextUpdate(object sender, EventArgs e)
        {
            CB_Districts.Items.Clear();
            //滚动到控件光标处 
            this.CB_Districts.SelectionStart = this.CB_Districts.Text.Length;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            CB_Districts.MaxDropDownItems = 8;
            Cursor = Cursors.Default;

            foreach (var item in Ldistricts)
            {
                if (item.districtName.Contains(CB_Districts.Text))
                {
                    CB_Districts.Items.Add(item);
                }
            }
            CB_Districts.DroppedDown = true;
        }

        private void StartSetDistrictCardNumber_FormClosed(object sender, FormClosedEventArgs e)
        {
            disposeAll.disposeAllStartForm();
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            AddCardCount();
            //createCardItems();
            //返回上一页
            this.Hide();
            if (Form1.startSetCardContent == null || Form1.startSetCardContent.IsDisposed)
            {
                Form1.startSetCardContent = new StartSetCardContent();

                Form1.startSetCardContent.Show();
            }
            else
            {
                Form1.startSetCardContent.Show();
            }
        }

        private void StartSetDistrictCardNumber_Activated(object sender, EventArgs e)
        {
            iniStartSetDistrictCardNumber();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            AddCardCount();
            //createCardItems();
            //去往下一页
            this.Hide();
            if (Form1.startSetUser == null || Form1.startSetUser.IsDisposed)
            {
                Form1.startSetUser = new StartSetUser();

                Form1.startSetUser.Show();

            }
            else
            {
                Form1.startSetUser.Show();
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

            for (int i = 0; i < AllCardName.Rows.Count; i++)
            {

                int j = 0;
                for (j = 0; j < DT_Cardcount.Rows.Count; j++)
                {
                    //先找地方
                    if (DT_Cardcount.Rows[j]["DistrictName"].ToString().Trim() == SelectDistrictName)
                    {
                        //再看具体的卡片//使用不变的cardcode,以防万一
                        if (DT_Cardcount.Rows[j]["cardCode"].ToString().Trim() == AllCardName.Rows[i]["cardCode"].ToString().Trim())
                        {
                            Dgv_CardCount.Rows.Add(AllCardName.Rows[i]["CardName"], DT_Cardcount.Rows[j]["CardMaxCount"], AllCardName.Rows[i]["CardCode"], AllCardName.Rows[i]["cardItemCount"]);
                            break;
                        }
                    }
                }
                //如果找不到,就加初始
                if (j == DT_Cardcount.Rows.Count)
                {
                    Dgv_CardCount.Rows.Add(AllCardName.Rows[i]["cardName"].ToString().Trim(), 0, AllCardName.Rows[i]["cardCode"].ToString().Trim(), AllCardName.Rows[i]["cardItemCount"].ToString().Trim());
                }

            }
        }

        //保存当前编辑的县
        private void AddCardCount()
        {
            //把焦点清空可以保存当前行
            Dgv_CardCount.CurrentCell = null;
            if (Cur_DistrictName == null)
            {
                return;
            }
            string SDistrictCode = "";
            string SDistrictName = "";
            //获取地址信息
            for (int l = 0; l < Ldistricts.Count; l++)
            {
                if (Ldistricts[l].districtName == Cur_DistrictName)
                {

                    SDistrictCode = Ldistricts[l].districtCode;
                    SDistrictName = Ldistricts[l].districtName;
                }
            }
            //根据这个县有多少卡片定每张卡多少分;
            double thiscards = 0.0;//有几类卡
            //算出卡片种类数
            for (int i = 0; i < Dgv_CardCount.Rows.Count; i++)
            {
                if (Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString() != "0")
                {
                    thiscards++;
                }
            }
            if (thiscards == 0)
            {
                CardScore = 0;
            }
            else
            {
                CardScore = Math.Round(StartAppraisal.SumScore / thiscards, 2);

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

                            if (Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString()=="0"|| Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString() == "")
                            {
                                DT_Cardcount.Rows[k]["CardItemScore"] = 0;
                            }
                            else
                            {
                                DT_Cardcount.Rows[k]["CardItemScore"] = Math.Round(CardScore / (double.Parse(Dgv_CardCount.Rows[i].Cells["CardItemCount"].Value.ToString()) * double.Parse(Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString())),2);
                            }
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
                    double SCardItemScore = 0.0;
                    ////生成分数，现在分数动态生成，所以默认为0就行，
                    //if (Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString() == "0" || Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString() == "")
                    //{
                     
                    //}
                    //else
                    //{
                    //    SCardItemScore = CardScore / (double.Parse(Dgv_CardCount.Rows[i].Cells["CardItemCount"].Value.ToString()) * double.Parse(Dgv_CardCount.Rows[i].Cells["CardNumber"].Value.ToString()));

                    //}


                    DT_Cardcount.Rows.Add(SDistrictCode, SDistrictName, SCardCode, SCardName, SCardMaxCount, SCardItemScore);
                }
            }
        }

        ////获取县内测评项目数量
        //private void createCardItems()
        //{
        //    DT_itemcount.Rows.Clear();
        //    DataTable dataTable = StartAppraisal.DT_CardListTable;

        //    for (int i=0;i<DT_Cardcount.Rows.Count; i++)
        //    {
        //        int k = 0;
        //        for (k = 0; k < DT_itemcount.Rows.Count; k++)
        //        {
        //            if(DT_Cardcount.Rows[i]["DistrictCode"] == DT_itemcount.Rows[k]["DistrictCode"])
        //            {
        //                for(int l =0;l< dataTable.Rows.Count; l++)
        //                {
        //                    if (DT_Cardcount.Rows[i]["CardCode"] == dataTable.Rows[l]["CardCode"])
        //                    {
        //                        DT_itemcount.Rows[k]["SumItem"] = int.Parse(DT_itemcount.Rows[k]["SumItem"].ToString()) + (int.Parse(DT_Cardcount.Rows[i]["CardMaxCount"].ToString()) * int.Parse(dataTable.Rows[l]["cardItemCount"].ToString()));
                                
        //                    }
        //                }

        //                break;
        //            }
        //        }
        //        if(k == DT_itemcount.Rows.Count)
        //        {
        //            for (int l = 0; l < dataTable.Rows.Count; l++)
        //            {
        //                if (DT_Cardcount.Rows[i]["CardCode"] == dataTable.Rows[l]["CardCode"])
        //                {
        //                    DT_itemcount.Rows.Add(DT_Cardcount.Rows[i]["DistrictCode"], DT_Cardcount.Rows[i]["DistrictName"], int.Parse(DT_Cardcount.Rows[i]["CardMaxCount"].ToString()) * int.Parse(dataTable.Rows[l]["cardItemCount"].ToString()));
        //                }
        //            }
                    
        //        }

        //    }
        //}



        private void Dgv_CardCount_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void Dgv_CardCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 22)
            //{
            //    StartAppraisal.PasteData(Dgv_CardCount);
            //}
        }
    }
}
