using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.BLL.Common
{
    public class FormValidation
    {
        public static bool ValidateInt(string id)
        {
            int num;
            bool succses = Int32.TryParse(id, out num);
            if (succses && num >= 0)
            {
                return true;
            }
            return false;
        }

        public static bool ValidateDecimal(string value)
        {
            decimal num;
            bool succses = Decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
            if (succses && num > 0)
            {
                return true;
            }

            return false;
        }
    }
}
