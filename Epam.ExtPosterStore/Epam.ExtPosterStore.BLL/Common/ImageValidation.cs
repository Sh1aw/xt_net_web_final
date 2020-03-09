using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.BLL.Common
{
    public class ImageValidation
    {
        static string[] WhiteList = new string[] { "image/jpg", "image/png", "image/jpeg" };
        private static int MaxSize = 3000000;
        public static bool Validate(string extention, int size)
        {
            if (WhiteList.Contains(extention) && size < MaxSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
