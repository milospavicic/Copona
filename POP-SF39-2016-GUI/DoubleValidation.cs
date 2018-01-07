using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF39_2016_GUI
{
    class DoubleValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = value as string;
            double doubleValue;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");

            if (string.IsNullOrEmpty(strValue))
                return new ValidationResult(false, "Ovo polje mora biti popunjeno.");
            try
            {
                doubleValue = Double.Parse(strValue);
                    if (doubleValue <= 0)
                        throw new Exception();
            }
            catch
            {
                return new ValidationResult(false, "Ovo polje mora biti pozitivan broj.");
            }
            return new ValidationResult(true, null);
        }
    }
}
