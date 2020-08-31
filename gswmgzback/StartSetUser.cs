using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static gswmgzback.UserTypeEnum;

namespace gswmgzback
{
    public partial class StartSetUser : Form
    {
        public StartSetUser()
        {
            InitializeComponent();
        }

        //没有分权限的用户信息表
        public static DataTable DT_SimpleUser;

        

        //下一页
        private void Btn_Next_Click(object sender, EventArgs e)
        {
            if (!AddSimpleUsr())
            {
                return;
            }

            //判断重复
            if (DT_SimpleUser.DefaultView.ToTable(true, "Account").Rows.Count < DT_SimpleUser.Rows.Count)
            {
                MessageBox.Show("有重复账号,请重新录入");
                return;
            }

            //显示下一页
            this.Hide();
            if (Form1.startSetUsrPermission == null || Form1.startSetUsrPermission.IsDisposed)
            {
                Form1.startSetUsrPermission = new StartSetUsrPermission();

                Form1.startSetUsrPermission.Show();

            }
            else
            {
                Form1.startSetUsrPermission.Show();
            }


        }

        private bool AddSimpleUsr()
        {
            //把焦点清空可以保存当前行
            Dgv_SimpleUser.CurrentCell = null;
            DT_SimpleUser.Clear();

            for (int i = 0; i < Dgv_SimpleUser.Rows.Count - 1; i++)
            {

                if (Dgv_SimpleUser.Rows[i].Cells[0].Value == null || Dgv_SimpleUser.Rows[i].Cells[1].Value == null || Dgv_SimpleUser.Rows[i].Cells[2].Value == null || Dgv_SimpleUser.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("请检查表格,确保无空值");
                    return false;
                }
                if (Dgv_SimpleUser.Rows[i].Cells[0].Value.ToString().ToUpper() == "ADMINISTRATOR")
                {
                    MessageBox.Show("不允许使用Administrator作为用户名");
                    return false;
                }
                DataRow dr = DT_SimpleUser.NewRow();
                dr["Account"] = Dgv_SimpleUser.Rows[i].Cells[0].Value;
                dr["Password"] = Dgv_SimpleUser.Rows[i].Cells[1].Value;
                dr["RealName"] = Dgv_SimpleUser.Rows[i].Cells[2].Value;
                switch (Dgv_SimpleUser.Rows[i].Cells[3].Value) 
                {
                    case "系统管理员":
                        dr["UsrType"] = UsrTypeE.系统管理员;
                        break;
                    case "测评带队":
                        dr["UsrType"] = UsrTypeE.测评带队;
                        break;
                    case "测评成员":
                        dr["UsrType"] = UsrTypeE.测评成员;
                        break;
                }
                //dr["UsrType"] = Dgv_SimpleUser.Rows[i].Cells[3].Value;


                DT_SimpleUser.Rows.Add(dr);
                
                
            }
           


            return true;
        }





        //上一页
        private void Btn_back_Click(object sender, EventArgs e)
        {
            if (!AddSimpleUsr())
            {
                return;
            }

            //显示上一页
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

        private void StartSetUser_Load(object sender, EventArgs e)
        {
            DT_SimpleUser = new DataTable();
            DT_SimpleUser.Columns.Add("Account", typeof(string));
            DT_SimpleUser.Columns.Add("Password", typeof(string));
            DT_SimpleUser.Columns.Add("RealName", typeof(string));
            DT_SimpleUser.Columns.Add("UsrType", typeof(int));
            //如果在配置测评时就显示下一步
            if (StartAppraisal.SumScore != 0)
            {
                Btn_Next.Visible = true;
                Btn_back.Visible = true;
                Btn_Delete.Visible = true;
                BTN_OK.Visible = false;
            }
            else//否则显示完成
            {
                Btn_Next.Visible = false;
                Btn_back.Visible = false;
                Btn_Delete.Visible = true;
                BTN_OK.Visible = true;
                string url = Form1.UrlPre + "ShowAllUser" + "?entrance=" + LogIn.entrance;
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
                    userInfoDatum userInfodatum = JsonConvert.DeserializeObject<userInfoDatum>(s);
                    DataRow dr = DT_SimpleUser.NewRow();
                    dr["Account"] = userInfodatum.Account;
                    dr["Password"] = userInfodatum.Password;
                    dr["RealName"] = userInfodatum.RealName;
                    dr["UsrType"] = userInfodatum.Type;

                    DT_SimpleUser.Rows.Add(dr);
                }

            }

            //DataGridViewComboBoxColumn myCombo = new DataGridViewComboBoxColumn();
            //myCombo.DataSource = new UsrTypeE[] { UsrTypeE.系统管理员, UsrTypeE.测评带队, UsrTypeE.测评成员 };
            //myCombo.HeaderText = "账号类型";
            //myCombo.Name = "UserType";

            //myCombo.ValueType = typeof(UsrTypeE);
            //myCombo.DataPropertyName = "UsrTypeE";
            //Dgv_SimpleUser.Columns.Add(myCombo);

        }

        private void iniSrartSetUser()
        {

            if (DT_SimpleUser.Rows.Count == 0)
            {
                return;
            }

            Dgv_SimpleUser.Rows.Clear();
            for(int i = 0; i < DT_SimpleUser.Rows.Count; i++)
            {
                string tempType = "";

                switch (DT_SimpleUser.Rows[i]["UsrType"])
                {
                    case 0:
                        tempType="系统管理员";
                        break;
                    case 1:
                        tempType = "测评带队";
                        break;
                    case 2:
                        tempType = "测评成员";
                        break;
                }
                Dgv_SimpleUser.Rows.Add(DT_SimpleUser.Rows[i]["Account"], DT_SimpleUser.Rows[i]["Password"], DT_SimpleUser.Rows[i]["RealName"],tempType);
               
            }
        }

        private void StartSetUser_Activated(object sender, EventArgs e)
        {
            iniSrartSetUser();
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要删除账号"+ Dgv_SimpleUser.CurrentRow.Cells["Account"].Value.ToString() + "吗?","删除", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Dgv_SimpleUser.Rows.Remove(Dgv_SimpleUser.CurrentRow);
                DT_SimpleUser.Rows.Clear();

                StartFinish.visitApi("deleteUser?Account=" + Dgv_SimpleUser.CurrentRow.Cells["Account"].Value.ToString().Trim());
            }
        }

        private void StartSetUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            disposeAll.disposeAllStartForm();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            string Account = "";
            string Password = "";
            string RealName = "";
            string DistrictCode = "";
            string DistrictName = "";
            int Type = 0;
            if (!AddSimpleUsr())
            {
                return;
            }

            //判断重复
            if (DT_SimpleUser.DefaultView.ToTable(true, "Account").Rows.Count < DT_SimpleUser.Rows.Count)
            {
                MessageBox.Show("有重复账号,请重新录入");
                return;
            }
            for (int i = 0; i < DT_SimpleUser.Rows.Count; i++)
            {

                Account = DT_SimpleUser.Rows[i]["Account"].ToString();
                Password = DT_SimpleUser.Rows[i]["Password"].ToString();
                RealName = DT_SimpleUser.Rows[i]["RealName"].ToString();
                DistrictCode = "";
                DistrictName = "";
                
                Type = int.Parse(DT_SimpleUser.Rows[i]["UsrType"].ToString());

                StartFinish.visitApi("SetUser?Account=" + Account + "&Password=" + Password + "&RealName=" + RealName + "&DistrictCode=" + DistrictCode + "&DistrictName=" + DistrictName + "&Type=" + Type + "&AddOrUpd=0");
            }
            MessageBox.Show("完成");

        }

        private void Dgv_SimpleUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 把第2列显示*号，*号的个数和实际数据的长度相同
            if (e.ColumnIndex == 1)
            {
                if (e.Value != null && e.Value.ToString().Length > 0)
                {
                    e.Value = new string('*', e.Value.ToString().Length);
                }
            }
        }

        private void Dgv_SimpleUser_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 编辑第2列时，把第2列显示为*号
            TextBox t = e.Control as TextBox;
            if (t != null)
            {
                if (this.Dgv_SimpleUser.CurrentCell.ColumnIndex == 1)
                    t.PasswordChar = '*';
                else
                    t.PasswordChar = new char();
            }
        }
    }
}
