using Fiscal_Management_System.model.validation_rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;

namespace Fiscal_Management_System_UnitTests.model.validation_rules
{
    [TestClass]
    public class TextLengthValidationRuleTest
    {
        public TextLengthValidationRuleTest() { }

        [TestMethod]
        public void CharLenLowerThanTwo()
        {
            TextLengthValidationRule text_val = new TextLengthValidationRule(2, 30);
            ValidationResult result = text_val.Validate("A", null);
            Assert.AreEqual(new ValidationResult(false, text_val.failure_message), result);
        }

        [TestMethod]
        public void CharLenHigherThanThirty()
        {
            TextLengthValidationRule text_val = new TextLengthValidationRule(2, 30);
            ValidationResult result = text_val.Validate("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", null);
            Assert.AreEqual(new ValidationResult(false, text_val.failure_message), result);
        }

        [TestMethod]
        public void CharLenIsEqualTwo()
        {
            TextLengthValidationRule text_val = new TextLengthValidationRule(2, 30);
            ValidationResult result = text_val.Validate("bb", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void CharLenIsEqualThirty()
        {
            TextLengthValidationRule text_val = new TextLengthValidationRule(2, 30);
            ValidationResult result = text_val.Validate("p.Brzęczyszczykiewicz Grzegorz", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void FieldIsNull()
        {
            TextLengthValidationRule text_val = new TextLengthValidationRule(2, 30);
            ValidationResult result = text_val.Validate(null, null);
            Assert.AreEqual(new ValidationResult(false, text_val.failure_message), result);
        }

        [TestMethod]
        public void CharLenIsWithinRange()
        {
            TextLengthValidationRule text_val = new TextLengthValidationRule(10, 15);
            ValidationResult result = text_val.Validate("Adwsdwjfhw 345", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }
    }
}
