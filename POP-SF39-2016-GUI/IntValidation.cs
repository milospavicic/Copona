using System;
using System.Globalization;
using System.Windows.Controls;

namespace POP_SF39_2016_GUI
{
    class IntValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = value as string;
            int intValue;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");

            if (string.IsNullOrEmpty(strValue))
                return new ValidationResult(false, "Ovo polje mora biti popunjeno.");
            try
            {
                intValue = int.Parse(strValue);
                if (intValue <= 0)
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
