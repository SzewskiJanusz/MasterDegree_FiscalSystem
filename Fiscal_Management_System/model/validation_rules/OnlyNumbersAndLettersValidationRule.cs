using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Fiscal_Management_System.model.validation_rules
{
    public class OnlyNumbersAndLettersValidationRule : ValidationRule
    {
        public string failure_message;
        public string PropertyName { get; set; }

        public OnlyNumbersAndLettersValidationRule() { }

        public OnlyNumbersAndLettersValidationRule(string thing)
        {
            PropertyName = thing;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            failure_message = "";

            if (value == null)
            {
                failure_message = "Wpisz " + PropertyName;
                return new ValidationResult(false, failure_message);
            }
            string str = value.ToString();

            if (str.Length == 0)
            {
                failure_message = "Wpisz " + PropertyName;
                return new ValidationResult(false, failure_message);
            }

            foreach (char c in str)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    failure_message = PropertyName + " może się składać tylko z dużych liter i cyfr";
                    return new ValidationResult(false, failure_message);
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
