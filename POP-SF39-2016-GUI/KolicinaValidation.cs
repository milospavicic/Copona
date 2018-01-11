using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace POP_SF39_2016_GUI
{
    public class KolicinaValidation : ValidationRule
    {
        private static int max;
        private static int vecUneto;
        public static int Max
        {
            get { return max; }
            set { max = value; }
        }
        public static int VecUneto
        {
            get { return vecUneto; }
            set { vecUneto = value; }
        }

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
            if (intValue + VecUneto > Max)
                return new ValidationResult(false," Nema dovoljno komada ovog namestaja.");
            return new ValidationResult(true, null);
        }
    }
}

