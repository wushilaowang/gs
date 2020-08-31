using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;

namespace gswmgzback
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }
        public static userInfoDatum userinfosDatum;
        public static string entrance = "";

        string CountSave_Path = System.IO.Directory.GetCurrentDirectory() + "\\wmcpsdkccount.txt";
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = TB_UserName.Text;
            string passWord = TB_PassWord.Text;
            entrance = CB_AppType.Text;
            string url = Form1.UrlPre + "Oblogin?account=" + userName+ "&password="+passWord + "&entrance=" + entrance + "&districtCode=0";
            string Httpres = HttpGet.HttpGetFunc(url);
            if (Httpres == null)
            {
                MessageBox.Show("网络未连接");
                this.Close();
                System.Environment.Exit(0);
            }
            //将字符串转换成json
            var Httpjsonresult = JObject.Parse(Httpres);
            //获取json中的data部分
            JToken HttpJsonvalue = Httpjsonresult.GetValue("errorMsg");
            string ErrorMsg = HttpJsonvalue.ToString();
            string[] accountContent = { userName, passWord, entrance };
            if (ErrorMsg != "成功")
            {
                MessageBox.Show(ErrorMsg);
            }
            else
            {

                //FileStream file = new FileStream(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName+"/countsave.txt",FileMode.OpenOrCreate);

                try
                {
                    if (File.Exists(CountSave_Path))
                    {
                       
                    }
                    else
                    {
                        File.Create(CountSave_Path);

                    }
                    File.WriteAllLines(CountSave_Path, accountContent);
                }
                catch { }


                HttpJsonvalue = Httpjsonresult.GetValue("data");

                userinfosDatum = JsonConvert.DeserializeObject<userInfoDatum>(HttpJsonvalue.ToString());

                if (userinfosDatum == null)
                {
                    userinfosDatum = new userInfoDatum();
                    userinfosDatum.Type = 0;
                }
                this.DialogResult = DialogResult.OK;
                this.Dispose();
                this.Close();
                //this.Hide();
            }

        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    string localHardwareAuthorization = ShowHardwareAuthorization.GetInfo();
            //    //获取服务器的硬件许可码进行对比
            //    string url = Form1.UrlPre + "CheckHardwareAuthorization?HardwareCode=" + localHardwareAuthorization;
            //    string Httpres = HttpGet.HttpGetFunc(url);
            //    if (Httpres == null)
            //    {
            //        MessageBox.Show("网络未连接");
            //        this.Close();
            //        System.Environment.Exit(0);
            //    }
            //    //将字符串转换成json
            //    var Httpjsonresult = JObject.Parse(Httpres);
            //    //获取json中的data部分
            //    JToken HttpJsonvalue = Httpjsonresult.GetValue("errorMsg");
            //    string ErrorMsg = HttpJsonvalue.ToString();
            //    if (ErrorMsg == "该硬件已授权!")
            //    {

            //    }
            //    else if (ErrorMsg == "该硬件授权已失效!")
            //    {
            //        Clipboard.SetDataObject(localHardwareAuthorization);
            //        MessageBox.Show("当前电脑授权已失效,请联系工作人员重新激活授权,您的硬件序列号是:" + localHardwareAuthorization + "\r\n 序列号已为您复制到剪切板,在您使用之前请请不要关闭此窗口");

            //        this.Close();
            //    }
            //    else
            //    {
            //        Clipboard.SetDataObject(localHardwareAuthorization);
            //        MessageBox.Show("当前电脑尚未获取授权,请联系工作人员为您授权后再进行登录,您的硬件序列号是:" + localHardwareAuthorization + "\r\n 序列号已为您复制到剪切板,在您使用之前请请不要关闭此窗口");
            //        this.Close();
            //    }
            //}
            //catch (NullReferenceException)
            //{

            //}
            try
            {
                if (File.Exists(CountSave_Path))
                {
                    TB_UserName.Text = File.ReadAllLines(CountSave_Path)[0];
                    TB_PassWord.Text= File.ReadAllLines(CountSave_Path)[1];
                    CB_AppType.Text = File.ReadAllLines(CountSave_Path)[2];

                }
                else
                {
                    File.Create(CountSave_Path);

                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}