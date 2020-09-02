using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace gswmgzback
{
    public partial class StartSetCardContent : Form
    {
        public StartSetCardContent()
        {
            InitializeComponent();
        }

        //卡片详细内容表 填入CardContent 里
        public static DataTable DT_CardContent ;

        //记录当前正在输入的卡片在表中的位置
        int Cur_index=-1;
        string Cur_Name = "";

        string Cur_CardCode = "";

        private void StartSetCardContent_Load(object sender, EventArgs e)
        {
            
            ////DataGridView无默认行
            Dgv_CardContent.AllowUserToAddRows = false;


            DT_CardContent = new DataTable();
            DT_CardContent.Columns.Add("cardCode", typeof(string));
            DT_CardContent.Columns.Add("item", typeof(string));
            DT_CardContent.Columns.Add("beizhu", typeof(string));
            DT_CardContent.Columns.Add("cardName", typeof(string));
            DT_CardContent.Columns.Add("score", typeof(string));
        }

        public void iniStarsSetCardContent()
        {
            //if (Cur_Name == Label_CardName.Text) {
            //    return;
            //}
            LB_CardNames.Items.Clear();
            DataTable CardsSum = StartAppraisal.DT_CardListTable;
            if (CardsSum.Rows.Count == 0)
            {
                LB_CardNames.Items.Add("尚未添加卡片");
            }

            for (int i = 0; i < CardsSum.Rows.Count; i++)
            {
                LB_CardNames.Items.Add(CardsSum.Rows[i][1].ToString().Trim());
            }
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {

            //没完成不保存
            if (ifCardOk())
            {
                AddContent();
            }

            //显示上一页
            this.Hide();
            if (Form1.startAppraisal == null || Form1.startAppraisal.IsDisposed)
            {
                Form1.startAppraisal = new StartAppraisal();

                Form1.startAppraisal.Show();

            }
            else
            {
                Form1.startAppraisal.Show();
            }
        }

        private void LB_CardNames_MouseClick(object sender, MouseEventArgs e)
        {
            
            
            //分站点
            int index = this.LB_CardNames.IndexFromPoint(e.Location);

            if(Cur_index == index)
            {
                return;
            }
            int cardRows = 0;
            if (index != ListBox.NoMatches &&  LB_CardNames.SelectedItem.ToString() != "尚未添加卡片")
            {
                //先保存当前的
                if (ifCardOk())
                {
                    AddContent();
                }
                
                Cur_index = index;
                Cur_Name = LB_CardNames.Text;

                Cur_CardCode = StartAppraisal.DT_CardListTable.Rows[index]["cardCode"].ToString();
                //
                Dgv_CardContent.Rows.Clear();
                
                string cardName = LB_CardNames.SelectedItem.ToString();
                Label_CardName.Text = cardName;
                //根据卡片选项数设置行数
                int LineIndex = -1;
                DataRow temp = ifExistInTable(StartAppraisal.DT_CardListTable, 1, cardName,out LineIndex);
                if (temp != null)
                {
                    cardRows = int.Parse(temp["cardItemCount"].ToString());
                    //如果已经填写过就显示卡片内容
                    temp = ifExistInTable(DT_CardContent,0,temp["cardCode"].ToString(), out LineIndex);
                    if (temp != null)
                    {
                        for (int a = 0; a < cardRows; a++,LineIndex++)
                        {
                            Dgv_CardContent.Rows.Add(DT_CardContent.Rows[LineIndex]["item"].ToString(), DT_CardContent.Rows[LineIndex]["beizhu"].ToString(), DT_CardContent.Rows[LineIndex]["score"].ToString());

                        }
                    }
                    else
                    {
                        for (int k = 0; k < cardRows; k++)
                        {
                            Dgv_CardContent.Rows.Add();
                        }
                    }
                }
            }
            else
            {
                LB_CardNames.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }


        public void AddContent()
        {
            //把焦点清空可以保存当前行
            Dgv_CardContent.CurrentCell = null; 
            if (Label_CardName.Text != "卡片名")
            {
                
                //根据卡片名获取卡片Code
                for(int i=0;i< StartAppraisal.DT_CardListTable.Rows.Count; i++)
                {
                    if(Label_CardName.Text.Substring(0, Label_CardName.Text.Length-1) == StartAppraisal.DT_CardListTable.Rows[i]["cardName"].ToString() )
                    {
                        Cur_CardCode = StartAppraisal.DT_CardListTable.Rows[i]["cardCode"].ToString();
                        break;
                    }
                }

                //如果已存在就删除旧的


                List<int> tempL = new List<int>();
                for (int l = 0; l < DT_CardContent.Rows.Count; l++)
                {
                    if (DT_CardContent.Rows[l]["cardCode"].ToString() == Cur_CardCode)
                    {
                        tempL.Add(l);
                    }

                }
                for (int k = 0; k < tempL.Count; k++)
                {
                    DT_CardContent.Rows[tempL[k]-k].Delete();

                }
                

                //添加新的
                for (int i = 0; i < Dgv_CardContent.RowCount; i++)
                {
                    DataRow row = DT_CardContent.NewRow();
                    row["cardCode"] = StartAppraisal.DT_CardListTable.Rows[Cur_index]["cardCode"];
                    row["item"] = Dgv_CardContent.Rows[i].Cells["CardContent"].Value.ToString();

                    row["beizhu"] = Dgv_CardContent.Rows[i].Cells["Beizhu"].Value==null?"": Dgv_CardContent.Rows[i].Cells["Beizhu"].Value.ToString();
                    row["score"] = Dgv_CardContent.Rows[i].Cells["score"].Value.ToString();
                    row["cardName"] = StartAppraisal.DT_CardListTable.Rows[Cur_index]["cardName"];

                    DT_CardContent.Rows.Add(row);
                }
            }
        }
        //当前卡片是否填写完成
        private bool ifCardOk()
        {
            for (int i = 0; i < Dgv_CardContent.RowCount; i++)
            {

                if (Dgv_CardContent.Rows[i].Cells["CardContent"].Value == null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 查看table中是否有要查找的值,若有返回该行,否则返回null
        /// </summary>
        /// <param name="dataTable">要查的表</param>
        /// <param name="colum">表的第几列</param>
        /// <param name="value">要查的值</param>
        /// <returns></returns>
        public DataRow ifExistInTable(DataTable dataTable,int colum,string value,out int index)
        {
            for(int i=0; i < dataTable.Rows.Count; i++)
            {//1看卡名，2看地县区分
                if (dataTable.Rows[i][colum].ToString() == value.Substring(0,value.Length-1)&& dataTable.Rows[i][colum+2].ToString() == value.Substring(value.Length - 1,1))
                {
                    index = i;
                    return dataTable.Rows[i];
                }
                if(dataTable.Rows[i][colum].ToString() == value)
                {
                    index = i;
                    return dataTable.Rows[i];
                }
            }
            index = -1;
            return null;
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            //先保存当前的
            if (ifCardOk())
            {
                AddContent();
            }

            //没完成不许进下一页
            if (StartAppraisal.NoReCardItemCount != DT_CardContent.Rows.Count)
            {
                MessageBox.Show("卡片内容未全部填写,请检查后重试");
                return;
            }
            foreach(DataRow item in DT_CardContent.Rows)
            {
                if (item["score"].ToString() == "")
                {
                    MessageBox.Show("有未填写的分值,请检查后重试");
                    return;
                }
                
            }
            
            //显示下一页
            this.Hide();
            if (Form1.startSetDistrictCardNumber == null || Form1.startSetDistrictCardNumber.IsDisposed)
            {
                Form1.startSetDistrictCardNumber = new StartSetDistrictCardNumber();

                Form1.startSetDistrictCardNumber.Show();

            }
            else
            {
                Form1.startSetDistrictCardNumber.Show();
            }
        }

        private void StartSetCardContent_FormClosed(object sender, FormClosedEventArgs e)
        {
            disposeAll.disposeAllStartForm();
        }

        private void StartSetCardContent_Shown(object sender, EventArgs e)
        {
            
        }

        private void StartSetCardContent_Enter(object sender, EventArgs e)
        {
           
        }

        private void StartSetCardContent_Activated(object sender, EventArgs e)
        {
            iniStarsSetCardContent();
        }

        private void LB_CardNames_MeasureItem(object sender, MeasureItemEventArgs e)
        {

        }

        private void LB_CardNames_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void Dgv_CardContent_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void Dgv_CardContent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 22)
            {
                StartAppraisal.PasteData(Dgv_CardContent);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DT_CardContent.Rows.Clear();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "表格文件(*.xls)|*.xls";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + file + ";" + "Extended Properties=Excel 8.0;";//Excel 8.0;HDR=no;IMEX=1
                using (var conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    System.Data.DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串
                    //string sql = string.Format("SELECT * FROM [{0}] WHERE [日期] is not null", firstSheetName); //查询字符串

                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, strConn);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    int k = 0;
                    for (int j = 0; j < StartAppraisal.DT_CardListTable.Rows.Count; j++)
                    {
                        for (int i = 0; i < int.Parse(StartAppraisal.DT_CardListTable.Rows[j]["cardItemCount"].ToString()); i++)
                        {
                            DataRow dr = DT_CardContent.NewRow();
                            dr["cardCode"] = StartAppraisal.DT_CardListTable.Rows[j]["cardCode"].ToString();
                            dr["cardName"] = StartAppraisal.DT_CardListTable.Rows[j]["cardName"].ToString();
                            dr["item"] = set.Tables[0].Rows[k][1].ToString();
                            dr["beizhu"] = set.Tables[0].Rows[k][2].ToString();
                            dr["score"] = set.Tables[0].Rows[k][3].ToString();
                            DT_CardContent.Rows.Add(dr);
                            k++;
                        }
                    }

                    Console.WriteLine(k);
                }

            }
        }

        private void LB_CardNames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
