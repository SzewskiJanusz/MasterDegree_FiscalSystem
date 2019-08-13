using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Fiscal_Management_System.model.validation_rules
{
    public class TextLengthValidationRule : ValidationRule
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string PropertyName { get; set; }

        public string failure_message;

        public TextLengthValidationRule() { }

        public TextLengthValidationRule(int min_len, int max_len)
        {
            MinLength = min_len;
            MaxLength = max_len;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Set failure message
            failure_message = PropertyName + " musi mieć od " + MinLength + " do " + MaxLength + " znaków";
            if (value == null)
                return new ValidationResult(false, failure_message);

            int len = value.ToString().Length;
            if (value.ToString().Length > MaxLength || value.ToString().Length < MinLength)
            {
                return new ValidationResult(false, failure_message);
            }

            return new ValidationResult(true, null);
        }
    }
}
