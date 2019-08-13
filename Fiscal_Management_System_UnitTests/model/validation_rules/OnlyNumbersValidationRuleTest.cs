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
    public class OnlyNumbersValidationRuleTest
    {
        public OnlyNumbersValidationRuleTest()
        { }

        [TestMethod]
        public void HasLetters()
        {
            OnlyNumbersValidationRule text_val = new OnlyNumbersValidationRule();
            ValidationResult result = text_val.Validate("24e", null);
            Assert.AreEqual(new ValidationResult(false, text_val.PropertyName + " może zawierać tylko cyfry"), result);
        }

        [TestMethod]
        public void EmptyValue()
        {
            OnlyNumbersValidationRule text_val = new OnlyNumbersValidationRule();
            ValidationResult result = text_val.Validate("", null);
            Assert.AreEqual(new ValidationResult(false, "Wpisz wartość"), result);
        }

        [TestMethod]
        public void ValueIsNull()
        {
            OnlyNumbersValidationRule text_val = new OnlyNumbersValidationRule();
            ValidationResult result = text_val.Validate(null, null);
            Assert.AreEqual(new ValidationResult(false, "Wpisz wartość"), result);
        }

        [TestMethod]
        public void HasNegativeNumber()
        {
            OnlyNumbersValidationRule text_val = new OnlyNumbersValidationRule();
            ValidationResult result = text_val.Validate("-321", null);
            Assert.AreEqual(new ValidationResult(false, text_val.PropertyName + " może zawierać tylko cyfry"), result);
        }

        [TestMethod]
        public void HasPositiveNumber()
        {
            OnlyNumbersValidationRule text_val = new OnlyNumbersValidationRule();
            ValidationResult result = text_val.Validate("33221", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void PositiveOverflow()
        {
            OnlyNumbersValidationRule text_val = new OnlyNumbersValidationRule();
            ValidationResult result = text_val.Validate("332212222222222222222", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void NegativeOverflow()
        {
            OnlyNumbersValidationRule text_val = new OnlyNumbersValidationRule();
            ValidationResult result = text_val.Validate("-21321321312321312312", null);
            Assert.AreEqual(new ValidationResult(false, text_val.PropertyName + " może zawierać tylko cyfry"), result);
        }
    }
}
