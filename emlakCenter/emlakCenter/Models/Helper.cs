using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RijndaelEncryptDecrypt;
using System.Web.Mvc;

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
        public static MvcHtmlString tarih(DateTime date)
        {
            return MvcHtmlString.Create((date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString()).ToString());
        }

        public static long UniqueIlan()
        {
            string c = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
                + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            return long.Parse(c);
        }
    }
}