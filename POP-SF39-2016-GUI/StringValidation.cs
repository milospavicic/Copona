using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF39_2016_GUI
{
    class StringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = value as string;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");

            if (string.IsNullOrEmpty(strValue))
                return new ValidationResult(false, "Ovo polje mora biti popunjeno.");
            else if (string.IsNullOrWhiteSpace(strValue))
                return new ValidationResult(false, "Ovo polje mora biti popunjeno.");

            var tempLength = strValue.Length;
            if(tempLength!=strValue.Trim().Length)
                return new ValidationResult(false, "Ovo polje ne sme imati razmake na pocetku i kraju");
            else
                return new ValidationResult(true, null);
        }
    }
}
