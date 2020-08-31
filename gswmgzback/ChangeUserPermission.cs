using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace gswmgzback
{
    public partial class SetUsersPermission : Form
    {

        List<userInfoDatum> userInfoDatas = new List<userInfoDatum>();
        List<DistrictInfoDatum> districtInfoDatas = new List<DistrictInfoDatum>();

        string CurAccount = "";


        public SetUsersPermission()
        {
            InitializeComponent();
        }

        private void ChangeUserPermission_Load(object sender, EventArgs e)
        {
            
        }

        private void iniChangeUserPermission()
        {
            userInfoDatas.Clear();
            dgv_user.Rows.Clear();
            CLB_Districts.Items.Clear();
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
                userInfoDatum userinfosDatum = JsonConvert.DeserializeObject<userInfoDatum>(s);
                userInfoDatas.Add(userinfosDatum);
                dgv_user.Rows.Add(userinfosDatum.Account, userinfosDatum.RealName, userinfosDatum.Type);
            }


            //获取所有县/区
            //ShowAlldistrict
            url = Form1.UrlPre + "ShowAlldistrict" + "?entrance=" + LogIn.entrance;
            Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("网络未连接");
                return;
            }
            //将字符串转换成json
            Httpjsonresult = JObject.Parse(Httpres);
            //获取json中的data部分
            HttpJsonvalue = Httpjsonresult.GetValue("data");
            resultrow = HttpJsonvalue.Count();

            for (int i = 0; i < resultrow; i++)
            {
                int column = HttpJsonvalue[i].Count();
                var s = HttpJsonvalue[i].ToString();
                DistrictInfoDatum districtInfoDatums = JsonConvert.DeserializeObject<DistrictInfoDatum>(s);
                districtInfoDatas.Add(districtInfoDatums);
                CLB_Districts.Items.Add(districtInfoDatums.DistrictName);
            }
            CurAccount = userInfoDatas[0].Account;
            setClbStatus(CurAccount);
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


        //根据当前选择的用户设置区域是否被选中
        private void setClbStatus(string Account)
        {
            //全部设为未选
            for (int j = 0; j < CLB_Districts.Items.Count; j++)
            {
                CLB_Districts.SetItemChecked(j, false);

            }
            ;
            var tempva = (userInfoDatas.Where(x => x.Account == Account).Select(x => x.DistrictName).FirstOrDefault());
            if (tempva == null)
            {

            }
            else
            {
                string[] usrDistrict = tempva.Split(',');
                for (int i = 0; i < usrDistrict.Count(); i++)
                {
                    for (int j = 0; j < CLB_Districts.Items.Count; j++)
                    {
                        if (CLB_Districts.Items[j].ToString() == usrDistrict[i])
                        {
                            CLB_Districts.SetItemChecked(j, true);
                            break;
                        }

                    }
                }
            }
           
           

        }
        //保存当前更改项
        private void saveUsrChanges()
        {
            var user = userInfoDatas.Where(x => x.Account == CurAccount).FirstOrDefault();
            userInfoDatas.Remove(user);
            user.DistrictCode = "";
            user.DistrictName = "";
            for (int i = 0; i < CLB_Districts.Items.Count; i++)
            {
                if (CLB_Districts.GetItemChecked(i))
                {
                    var temp = districtInfoDatas.Where(x => x.DistrictName == CLB_Districts.Items[i].ToString()).FirstOrDefault();
                    user.DistrictCode += temp.DistrictCode + ",";
                    user.DistrictName += temp.DistrictName + ",";
                }
            }
            if (user.DistrictCode.Length > 0)
            {
                user.DistrictCode = user.DistrictCode.Substring(0, user.DistrictCode.Length - 1);
                user.DistrictName = user.DistrictName.Substring(0, user.DistrictName.Length - 1);
            }
            userInfoDatas.Add(user);

        }


        private void dgv_user_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void dgv_user_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            saveUsrChanges();

            int index = e.RowIndex;
            if (index >= 0)
            {
                CurAccount = dgv_user.Rows[index].Cells[0].Value.ToString();
                setClbStatus(CurAccount);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveUsrChanges();
            //userInfoDatas
            updateUsrChanges();

        }

        private void updateUsrChanges()
        {
            foreach(var item in userInfoDatas)
            {
                string url = Form1.UrlPre + "SetUser?Account="+item.Account+ "&Password="+item.Password + "&RealName=" + item.RealName + "&DistrictCode=" + item.DistrictCode + "&DistrictName=" + item.DistrictName + "&Type=" + item.Type + "&entrance=" + LogIn.entrance + "&AddOrUpd=0";
                string Httpres = HttpGet.HttpGetFunc(url);
                if (Httpres == null)
                {
                    MessageBox.Show("服务异常");
                    return;
                }
            }

            MessageBox.Show("成功!");
        }

        private void SetUsersPermission_Activated(object sender, EventArgs e)
        {
            iniChangeUserPermission();
        }
    }
}
