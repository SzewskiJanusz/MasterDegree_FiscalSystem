using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Fiscal_Management_System.model.validation_rules
{
    public class GrandLetterFirstValidationRule : ValidationRule
    {
        public string failure_message;
        public string PropertyName { get; set; }

        public GrandLetterFirstValidationRule() { }

        public GrandLetterFirstValidationRule(string thing)
        {
            PropertyName = thing;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            failure_message = PropertyName + " musi zaczynać się wielką literą";

            if (value == null)
                return new ValidationResult(false, failure_message);

            if (value.ToString().Length == 0)
            {
                return new ValidationResult(false, failure_message);
            }


            if (Char.IsUpper(value.ToString()[0]))
            {
                return new ValidationResult(true, null);
            }

            return new ValidationResult(false, failure_message);
        }
    }
}
