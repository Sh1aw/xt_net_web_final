using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Epam.ExtPosterStore.WebPagesPL.Common
{
    public class ImageToBinaryConverter
    {
        public static byte[] ConvertToBinary(HttpPostedFileBase file)
        {
                if (file != null)
                {
                    byte[] data = new byte[file.ContentLength];
                    Stream fs = file.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    fs.Close();
                    return bytes;
                }
                else
                {
                    return null;
                }
        }

        public static string DeconvertToImage(byte[] imgBytes)
        {
                return Convert.ToBase64String(imgBytes);
        }
    }
}