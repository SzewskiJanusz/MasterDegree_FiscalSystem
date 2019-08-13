using System;
using System.Globalization;
using System.Windows.Controls;

namespace Fiscal_Management_System.model.validation_rules
{
    public class NoLettersValidationRule : ValidationRule
    {
        public string failure_message;
        public string PropertyName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            failure_message = "Wpisz wartość";

            if (value == null)
                return new ValidationResult(false, failure_message);

            string str = value.ToString();
            if (str.Length == 0)
                return new ValidationResult(false, failure_message);

            foreach (char c in str)
            {
                if (Char.IsLetter(c))
                {
                    failure_message = PropertyName + " nie może zawierać liter";
                    return new ValidationResult(false, failure_message);
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
