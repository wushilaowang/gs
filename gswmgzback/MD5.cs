using System;
using System.Security.Cryptography;
using System.Text;

namespace gswmgzback
{
    public class MD5
    {
        //md5加密
        public static string MD5Encrypt(string normalTxt)
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);//求Byte[]数组
            var Md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);//求哈希值
            return Convert.ToBase64String(Md5);//将Byte[]数组转为净荷明文(其实就是字符串)
        }
        
    }
}
