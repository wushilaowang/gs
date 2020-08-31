using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gswmgzback
{
    public partial class SumScoreForm : Form
    {
        public SumScoreForm()
        {
            InitializeComponent();
        }

        private void SumScoreForm_Load(object sender, EventArgs e)
        {
            var LCardScore = ResultShow.PUBLCardScore.Where(x=>x.DistrictName== ResultShow.PUBdistrictName).ToList();
            var LCardOutlins = ResultShow.PUBLCardOutlins;
            DataTable dt = new DataTable();//建立个数据表
            dt.Columns.Add(new DataColumn("测评点", typeof(string)));
            for (int i = 0; i < LCardOutlins.Count; i++)
            {
                dt.Columns.Add(new DataColumn(LCardOutlins[i].CardName, typeof(string)));//在表中添加int类型的列
            }
            dt.Columns.Add(new DataColumn("合计", typeof(string)));
            DataRow dr ;
            string curname = "";
            string localName = "";
            List<string> places = new List<string>();

            for (int i = 0; i < LCardScore.Count; i++)
            {
                if (LCardScore[i].InputName.Contains("问卷"))
                {
                    localName = LCardScore[i].InputName.Substring(0, LCardScore[i].InputName.Length-4);
                }
                else
                {
                    localName = LCardScore[i].InputName;
                }

                if (curname != localName)
                {
                    if (places.Contains(localName))
                    {
                        continue;
                    }
                    else
                    {
                        places.Add(localName);
                    }
                    dr = dt.NewRow();
                    curname = localName;
                    double? sum = 0; 
                    for (int j = 0; j < LCardOutlins.Count; j++)
                    {
                        try
                        {
                            if (LCardOutlins[j].CardName.Contains("问卷"))
                            {
                                curname += "调查问卷";

                            }
                            dr[LCardOutlins[j].CardName] = LCardScore.Where(x => x.InputName==(curname) && x.CardCode == LCardOutlins[j].CardCode).Select(x => x.Sum).First();
                            sum += LCardScore.Where(x => x.InputName==(curname) && x.CardCode == LCardOutlins[j].CardCode).Select(x => x.Sum).First();


                            curname = localName;
                        }
                        catch (Exception)
                        {
                            curname = localName;
                            dr[LCardOutlins[j].CardName] = 0;
                            sum += 0;
                        }
                    }
                    dr["合计"] = sum;
                    dr["测评点"] = curname;
                    dt.Rows.Add(dr);
                }
                else
                {
                    continue;
                }
                
            }

            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File To";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
            { return; }
            string saveFileName = saveFileDialog.FileName;
            if (dataGridView1.Rows.Count > 0)
            {
                //string saveFileName = "";
                ////bool fileSaved = false;  
                //SaveFileDialog saveDialog = new SaveFileDialog();
                //saveDialog.DefaultExt = "xls";
                //saveDialog.Filter = "Excel文件|*.xls";
                //saveDialog.FileName = fileName;
                //saveDialog.ShowDialog();
                //saveFileName = saveDialog.FileName;
                //if (saveFileName.IndexOf(":") < 0) return; //被点了取消
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }

                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1  

                //写入标题  
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                //String mont = jiezhi_date.Value.GetDateTimeFormats('s')[0].ToString().Substring(5, 2);
                //mont = mont.Replace("/", "");
                //int month = int.Parse(mont);
                //写入数值  
                for (int r = 0; r < dataGridView1.Rows.Count; r++)
                {

                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        //String xingzhi = dataGridView1.Rows[r].Cells[2].Value.ToString();
                        //if (month < 11 && xingzhi == "非居民用户" || month < 11 && xingzhi == "非居民直供" || month > 10)
                        worksheet.Cells[r + 2, i + 1] = dataGridView1.Rows[r].Cells[i].Value;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应  
                                                         //   if (Microsoft.Office.Interop.cmbxType.Text != "Notification")  
                                                         //   {  
                                                         //       Excel.Range rg = worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[ds.Tables[0].Rows.Count + 1, 2]);  
                                                         //      rg.NumberFormat = "00000000";  
                                                         //   }  

                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }

                }

                xlApp.Quit();
                GC.Collect();//强行销毁   
                             // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL  
                MessageBox.Show("导出文件成功", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("报表为空,无表格需要导出", "提示", MessageBoxButtons.OK);
            }
        }
    }
}
