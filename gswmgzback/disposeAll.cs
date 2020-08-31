using System.Windows.Forms;

namespace gswmgzback
{
    class disposeAll
    {
        public static void disposeAllStartForm()
        {
            disposeOnePage(Form1.startAppraisal);
            disposeOnePage(Form1.startSetUser);
            disposeOnePage(Form1.startSetCardContent);
            disposeOnePage(Form1.startSetDistrictCardNumber);
            disposeOnePage(Form1.startSetUsrPermission);
            disposeOnePage(Form1.startFinish);
        }



        public static void disposeOnePage(Form form)
        {
            if (form != null && !form.IsDisposed)
            {
                form.Dispose();
            }
        }
    }
}
