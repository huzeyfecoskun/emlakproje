using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RijndaelEncryptDecrypt;

namespace emlakCenter.Models
{
    public static class Helper
    {
        public static string encrypt(string data)
        {
            return EncryptDecryptUtils.Encrypt(data, "xWertggsdfgbv", "fdsfsaEFK", "SHA1");
        }

        public static string decrypt(string data)
        {
            return EncryptDecryptUtils.Decrypt(data, "xWertggsdfgbv", "fdsfsaEFK", "SHA1");
        }

        public static int ilanNo()
        {
            int ilanNumarasi;
            ilanNumarasi = 100;
            return ilanNumarasi;
        }
    }
}