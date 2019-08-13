using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Fiscal_Management_System.model.validation_rules
{
    public class OnlyNumbersValidationRule : ValidationRule
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
                if (c < '0' || c > '9')
                {
                    failure_message = PropertyName + " może zawierać tylko cyfry";
                    return new ValidationResult(false, failure_message);
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
