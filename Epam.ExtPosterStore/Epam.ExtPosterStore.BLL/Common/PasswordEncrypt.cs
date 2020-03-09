using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.BLL.Common
{
    public static class PasswordEncrypt
    {
        internal static string GetMD5HashData(string data)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();

            var dataBytes = encoder.GetBytes(data);
            var daBytes = md5Hasher.ComputeHash(dataBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < daBytes.Length; i++)
            {
                sb.Append(daBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        internal static bool checkHashPassword(string hash, string original)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();

            var dataBytes = encoder.GetBytes(original);
            var daBytes = md5Hasher.ComputeHash(dataBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < daBytes.Length; i++)
            {
                sb.Append(daBytes[i].ToString("x2"));
            }
            var originalHash = sb.ToString();
            if (hash.Equals(originalHash))
            {
                return true;
            }

            return false;
        }
    }
}
