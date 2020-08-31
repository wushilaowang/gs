using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static gswmgzback.AlldistrictContent;

namespace gswmgzback
{
    public partial class StartSetUsrPermission : Form
    {
        public StartSetUsrPermission()
        {
            InitializeComponent();
        }

        public static DataTable DT_UserInfo;

        
        public string Cur_UsrName = "";
        public List<AlldistrictDatum> districts;

        private void StartSetUsrPermission_Load(object sender, EventArgs e)
        {
            DT_UserInfo = new DataTable();
            DT_UserInfo.Columns.Add("Account", typeof(string));
            DT_UserInfo.Columns.Add("Password", typeof(string));
            DT_UserInfo.Columns.Add("RealName", typeof(string));
            DT_UserInfo.Columns.Add("DistrictCode", typeof(string));
            DT_UserInfo.Columns.Add("DistrictName", typeof(string));
            DT_UserInfo.Columns.Add("Type", typeof(int)); 



        }

        private void iniStartSetUsrPermission()
        {
            if(Cur_UsrName == label_UserName.Text)
            {
                return;
            }

            districts = StartSetDistrictCardNumber.Ldistricts;
            LB_Usrs.Items.Clear();
            CLB_Districts.Items.Clear();


            for (int i = 0; i < StartSetUser.DT_SimpleUser.Rows.Count; i++)
            {
                int k = 0;
                for ( k = 0; k < DT_UserInfo.Rows.Count; k++)
                {
                    if (DT_UserInfo.Rows[k]["Account"].ToString().Trim()==StartSetUser.DT_SimpleUser.Rows[i]["Account"].ToString().Trim())
                    {
                        break;
                    }
                    
                }

                if (k== DT_UserInfo.Rows.Count)
                {
                    DataRow dr = DT_UserInfo.NewRow();
                    dr["Account"] = StartSetUser.DT_SimpleUser.Rows[i]["Account"];
                    dr["Password"] = StartSetUser.DT_SimpleUser.Rows[i]["Password"];
                    dr["RealName"] = StartSetUser.DT_SimpleUser.Rows[i]["RealName"];
                    dr["DistrictCode"] = StartSetUser.DT_SimpleUser.Rows[i]["UsrType"];
                    dr["DistrictName"] = null;
                    dr["Type"] = StartSetUser.DT_SimpleUser.Rows[i]["UsrType"];
                    
                    DT_UserInfo.Rows.Add(dr);

                }
                LB_Usrs.Items.Add(StartSetUser.DT_SimpleUser.Rows[i]["RealName"].ToString().Trim());
            }

            NewOrShow();
        }

        private void NewOrShow()
        {
            for (int j = 0; j < districts.Count; j++)
            {
                CLB_Districts.Items.Add(districts[j].districtName);

            }
            for (int i = 0; i < DT_UserInfo.Rows.Count; i++)
            {
                if (DT_UserInfo.Rows[i]["RealName"].ToString().Trim() == Cur_UsrName)
                {
                    //分过就查询
                    if (DT_UserInfo.Rows[i]["DistrictName"].ToString().Trim() != "")
                    {
                        //获取districtName,分成数组,一个一个查询index,然后把clb里的选选个画勾
                        string[] SelectedDistricts = DT_UserInfo.Rows[i]["DistrictName"].ToString().Trim().Split(',');

                        for (int k = 0; k < SelectedDistricts.Count(); k++)
                        {
                            for (int l = 0; l < CLB_Districts.Items.Count; l++)
                            {
                                if (SelectedDistricts[k] == CLB_Districts.Items[l].ToString().Trim())
                                {
                                    CLB_Districts.SetItemChecked(l, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LB_Usrs_MouseClick(object sender, MouseEventArgs e)
        {
            if(Cur_UsrName == LB_Usrs.Text|| Cur_UsrName == "")
            {
                Cur_UsrName = LB_Usrs.Text;
                label_UserName.Text = LB_Usrs.Text;
                return;
            }

            saveUserInfo();


            int index = this.LB_Usrs.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                CLB_Districts.Items.Clear();
                Cur_UsrName = LB_Usrs.Text;

                NewOrShow();
            }
            else
            {
                LB_Usrs.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }

        private void saveUserInfo()
        {
            for(int i=0;i< DT_UserInfo.Rows.Count; i++)
            {
                
                if (DT_UserInfo.Rows[i]["RealName"].ToString().Trim() == Cur_UsrName)
                {
                    DT_UserInfo.Rows[i]["DistrictCode"] = DT_UserInfo.Rows[i]["Type"];
                    DT_UserInfo.Rows[i]["DistrictName"] = null;

                    for (int j = 0; j < CLB_Districts.Items.Count; j++)
                    {
                        if (CLB_Districts.GetItemChecked(j))
                        {
                            DT_UserInfo.Rows[i]["DistrictCode"] += districts[j].districtCode+",";
                            DT_UserInfo.Rows[i]["DistrictName"] += districts[j].districtName+",";
                        }
                    }
                    if (DT_UserInfo.Rows[i]["DistrictName"].ToString().Trim().Length > 0)
                    {
                        DT_UserInfo.Rows[i]["DistrictCode"] = DT_UserInfo.Rows[i]["DistrictCode"].ToString().Trim().Substring(1, DT_UserInfo.Rows[i]["DistrictCode"].ToString().Trim().Length - 2);
                        DT_UserInfo.Rows[i]["DistrictName"] = DT_UserInfo.Rows[i]["DistrictName"].ToString().Trim().Substring(0, DT_UserInfo.Rows[i]["DistrictName"].ToString().Trim().Length - 1);
                    }
                    
                    break;
                }
            }

        }

        private void CLB_Districts_MouseClick(object sender, MouseEventArgs e)
        {
            int index = this.CLB_Districts.IndexFromPoint(e.Location);
            if (index != CheckedListBox.NoMatches)
            {
                CLB_Districts.SetItemChecked(index, !CLB_Districts.GetItemChecked(index));
            }
            else
            {
                CLB_Districts.SelectedIndex = -1;//不做任何操作，将ListBox的选中项取消
            }
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            saveUserInfo();
            //上一页
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

        private void StartSetUsrPermission_Activated(object sender, EventArgs e)
        {
            iniStartSetUsrPermission();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            saveUserInfo();
            //下一页
            this.Hide();
            if (Form1.startFinish == null || Form1.startFinish.IsDisposed)
            {
                Form1.startFinish = new StartFinish();

                Form1.startFinish.Show();

            }
            else
            {
                Form1.startFinish.Show();
            }
        }

        private void StartSetUsrPermission_FormClosed(object sender, FormClosedEventArgs e)
        {
            disposeAll.disposeAllStartForm();
        }
    }
}
