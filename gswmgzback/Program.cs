using System;
using System.Net;
using System.Windows.Forms;

namespace gswmgzback
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            HttpWebRequest.DefaultWebProxy = null;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogIn logIn = new LogIn();
            logIn.ShowDialog();
            if (logIn.DialogResult == DialogResult.OK)
            {
                logIn.Dispose();
                Application.Run(new Form1());
            }
            else if (logIn.DialogResult == DialogResult.Cancel)
            {
                logIn.Dispose();
                return;
            }


            //Application.Run(new Form1());
            //Application.Run(new StartFinish());
        }
    }
}
