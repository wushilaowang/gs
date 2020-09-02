using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace gswmgzback
{
    public partial class StartAppraisal : Form
    {
        public StartAppraisal()
        {
            InitializeComponent();
        }
        //记录总卡片存入cardoutlin
        public static DataTable DT_CardListTable = new DataTable(); 

        //记录每张卡片的分值//每一类卡片分值都是相同的
        public double CardScore = 0.0;

        //记录总分
        public static int SumScore = 0;
        //记录无重复项目数
        public static int NoReCardItemCount = 0;

        //记录无重复卡片数
        public double NoReCardCount = 0.0;

        public static string AppraisalType = "";

        private void DG_CardList_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
           

        }

        private void StartAppraisal_Load(object sender, EventArgs e)
        {
            try
            {
                
                DT_CardListTable.Columns.Add("cardCode", typeof(string));
                DT_CardListTable.Columns.Add("cardName", typeof(string));
                DT_CardListTable.Columns.Add("cardItemCount", typeof(int));

            }
            catch(DuplicateNameException ex)
            {

            }
            

            //点击即可编辑
            DG_CardList.EditMode = DataGridViewEditMode.EditOnEnter;
            ////DataGridView无默认行
            //DG_CardList.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            //把焦点清空可以保存当前行
            DG_CardList.CurrentCell = null;
            //NoReCardCount = DG_CardList.Rows.Count - 1;
            NoReCardItemCount = 0;
            DT_CardListTable.Clear();
            try
            {
                SumScore = int.Parse(TB_SumScore.Text);
                if (SumScore == 0)
                {
                    MessageBox.Show("总分为0");
                    return;
                }
            }catch(Exception)
            {
                MessageBox.Show("总分必须是数字");
                return;
            }

            AppraisalType = CB_AppraisalType.Text;
            if (CB_AppraisalType.Text == "")
            {
                MessageBox.Show("尚未选择测评类型");
                return;
            }

            int cardcCodeNum = 1;
            int wjCodeNum = 1;
            for (int i = 0; i < DG_CardList.Rows.Count - 1; i++)
            {
                if (DG_CardList.Rows[i].Cells[0].Value == null || DG_CardList.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("请检查表格,确保无空值");
                    return;
                }
                int temp = 0;
                int.TryParse(DG_CardList.Rows[i].Cells[1].Value.ToString(),out temp);
                if(temp == 0)
                {
                    MessageBox.Show("选项数量必须是数字");
                    return;
                }
                NoReCardItemCount += temp;
                DataRow dr = DT_CardListTable.NewRow();
                if (DG_CardList.Rows[i].Cells[0].Value.ToString().Contains("问卷"))
                {
                    if(wjCodeNum<10)
                        dr["cardCode"] = "K900"+wjCodeNum;
                    else
                        dr["cardCode"] = "K90" + wjCodeNum;
                    wjCodeNum++;
                }
                else if (cardcCodeNum < 10)
                {
                    dr["cardCode"] = "K010" + (cardcCodeNum);
                    cardcCodeNum++;
                }
                else
                {
                    dr["cardCode"] = "K01" + (cardcCodeNum);
                    cardcCodeNum++;
                }
                dr["cardName"] = DG_CardList.Rows[i].Cells[0].Value.ToString();
                dr["cardItemCount"] = DG_CardList.Rows[i].Cells[1].Value;
                DT_CardListTable.Rows.Add(dr);
            }
            NoReCardCount = DT_CardListTable.Rows.Count;
            if (NoReCardCount == 0)
            {
                MessageBox.Show("未输入任何卡片信息");
                return;
            }

            
            ////判断重复
            //Console.WriteLine(DT_CardListTable.DefaultView.ToTable(true, "cardName").Rows.Count);
            //if (DT_CardListTable.DefaultView.ToTable(true, "cardName").Rows.Count < DT_CardListTable.Rows.Count)
            //{
            //    MessageBox.Show("有重复项目,请重新录入");
            //    return;
            //}

            this.Hide();
            //显示下一页
            if (Form1.startSetCardContent == null || Form1.startSetCardContent.IsDisposed)
            {
                Form1.startSetCardContent = new StartSetCardContent();
                Form1.startSetCardContent.Show();
            }
            else
            {
                Form1.startSetCardContent.iniStarsSetCardContent();
                Form1.startSetCardContent.Show();
            }
            
        }

        private void StartAppraisal_FormClosed(object sender, FormClosedEventArgs e)
        {
            disposeAll.disposeAllStartForm();
        }

        private void B_Click(object sender, EventArgs e)
        {
            DialogResult dr =  MessageBox.Show("确定要删除选中行吗?", "删除", MessageBoxButtons.YesNo);
            if(dr == DialogResult.Yes)
            {
                DG_CardList.Rows.Remove(DG_CardList.CurrentRow);
            }
        }

        private void DG_CardList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void DG_CardList_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
        public static void PasteData(DataGridView dataGridView)
        {
            string clipboardText = Clipboard.GetText(); //获取剪贴板中的内容
            if (string.IsNullOrEmpty(clipboardText))
            {
                return;
            }
            int colnum = 0;
            int rownum = 0;
            for (int i = 0; i < clipboardText.Length; i++)
            {
                if (clipboardText.Substring(i, 1) == "\t")
                {
                    colnum++;
                }
                if (clipboardText.Substring(i, 1) == "\n")
                {
                    rownum++;
                }
            }
            colnum = colnum / rownum + 1;
            int selectedRowIndex, selectedColIndex;
            selectedRowIndex = dataGridView.CurrentRow.Index;
            selectedColIndex = dataGridView.CurrentCell.ColumnIndex;
            if (selectedRowIndex + rownum > dataGridView.RowCount || selectedColIndex + colnum > dataGridView.ColumnCount)
            {
                MessageBox.Show("粘贴区域大小不一致");
                return;
            }
            String[][] temp = new String[rownum][];
            for (int i = 0; i < rownum; i++)
            {
                temp[i] = new String[colnum];
            }
            int m = 0, n = 0, len = 0;
            while (len != clipboardText.Length)
            {
                String str = clipboardText.Substring(len, 1);
                if (str == "\t")
                {
                    n++;
                }
                else if (str == "\n")
                {
                    m++;
                    n = 0;
                }
                else
                {
                    temp[m][n] += str;
                }
                len++;
            }
            for (int i = selectedRowIndex; i < selectedRowIndex + rownum; i++)
            {
                for (int j = selectedColIndex; j < selectedColIndex + colnum; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = temp[i - selectedRowIndex][j - selectedColIndex];
                }
            }
        }

        private void DG_CardList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteData(DG_CardList);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("卡片名称(必填)", typeof(string));
            dataTable.Columns.Add("卡片内选项数量(必填)", typeof(int));
            DG_CardList.Columns.Clear();
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "表格文件(*.xls)|*.xls";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + file + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
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
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        if (set.Tables[0].Rows[i][0].ToString() == "")
                        {
                            break;
                        }
                        DataRow dr = dataTable.NewRow();
                        dr["卡片名称(必填)"] = set.Tables[0].Rows[i][0].ToString();
                        dr["卡片内选项数量(必填)"] = int.Parse(set.Tables[0].Rows[i][1].ToString());
                        dataTable.Rows.Add(dr);
                    }
                    DG_CardList.DataSource = dataTable;
                }
            }
        }  
    }
}
