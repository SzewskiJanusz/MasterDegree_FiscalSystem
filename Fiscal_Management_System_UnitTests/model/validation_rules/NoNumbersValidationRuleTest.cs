using Fiscal_Management_System.model.validation_rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fiscal_Management_System_UnitTests.model.validation_rules
{
    [TestClass]
    public class NoNumbersValidationRuleTest
    {
        public NoNumbersValidationRuleTest() { }

        [TestMethod]
        public void HasDigits()
        {
            NoNumbersValidationRule text_val = new NoNumbersValidationRule();
            ValidationResult result = text_val.Validate("Joanna1", null);
            Assert.AreEqual(new ValidationResult(false, text_val.PropertyName + " nie może zawierać cyfr"), result);
        }

        [TestMethod]
        public void HasNoDigits()
        {
            NoNumbersValidationRule text_val = new NoNumbersValidationRule();
            ValidationResult result = text_val.Validate("Joanna Krupowska", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasSpecialChar()
        {
            NoNumbersValidationRule text_val = new NoNumbersValidationRule();
            ValidationResult result = text_val.Validate("Joanna Krupows#*ka", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void ValueIsEmpty()
        {
            NoNumbersValidationRule text_val = new NoNumbersValidationRule();
            ValidationResult result = text_val.Validate("", null);
            Assert.AreEqual(new ValidationResult(false, "Wpisz wartość"), result);
        }

        [TestMethod]
        public void ValueIsNull()
        {
            NoNumbersValidationRule text_val = new NoNumbersValidationRule();
            ValidationResult result = text_val.Validate(null, null);
            Assert.AreEqual(new ValidationResult(false, "Wpisz wartość"), result);
        }

    }
}
