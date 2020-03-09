using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Epam.ExtPosterStore.WebPagesPL.Common
{
    public class InputNormalizer
    {
        public static bool ValidateInt(string id)
        {
            int num;
            bool succses = Int32.TryParse(id, out num);
            if (succses && num>=0)
            {
                return true;
            }
            return false;
        }

        public static bool ValidateDecimal(string value)
        {
            decimal num;
            bool succses = Decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            if (succses && num>0)
            {
                return true;
            }

            return false;
        }

        public static decimal  SwitchCulture(decimal val)
        {
            val = Convert.ToDecimal(val, new CultureInfo("en-US"));
            return val;
        }
    }
}