using System.Management;

namespace gswmgzback
{
    class ShowHardwareAuthorization
    {
        //返回电脑cpu和硬盘序列号
        public static string GetInfo()
        {
            string cpuSerialCode = "";//cpu序列号
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuSerialCode = mo.Properties["ProcessorId"].Value.ToString();
            }

            ////获取硬盘ID
            //String HDid = "";
            //ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            //ManagementObjectCollection moc1 = cimobject1.GetInstances();
            //foreach (ManagementObject mo in moc1)
            //{
            //    HDid = (string)mo.Properties["Model"].Value;
            //}

            ManagementObjectSearcher mos = new ManagementObjectSearcher();
            mos.Query = new SelectQuery("Win32_DiskDrive", "", new string[] { "PNPDeviceID", "Signature" });
            ManagementObjectCollection myCollection = mos.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = myCollection.GetEnumerator();
            em.MoveNext();
            ManagementBaseObject moo = em.Current;
            string HDSerialCode = moo.Properties["signature"].Value.ToString().Trim();
            

            string result = cpuSerialCode + HDSerialCode;
            return result;
        }

    }

}
