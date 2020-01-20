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
    public class GrandLetterFirstValidationRuleTest
    {
        public GrandLetterFirstValidationRuleTest()
        { }

        [TestMethod]
        public void FirstLetterIsGrand()
        {
            // Arrange
            GrandLetterFirstValidationRule rule =
                new GrandLetterFirstValidationRule();

            // Act
            ValidationResult result = rule.Validate("Aleksander", null);

            // Assert
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void FirstLetterIsLower()
        {
            GrandLetterFirstValidationRule rule =
                new GrandLetterFirstValidationRule("Imię");
            ValidationResult result = rule.Validate("aleksander", null);
            Assert.AreEqual(new ValidationResult(false, rule.failure_message), result);
        }

        [TestMethod]
        public void FirstLetterIsDigit()
        {
            GrandLetterFirstValidationRule rule =
                new GrandLetterFirstValidationRule("Imię");
            ValidationResult result = rule.Validate("1aleksander", null);
            Assert.AreEqual(new ValidationResult(false, rule.failure_message), result);
        }

        [TestMethod]
        public void ThereIsNoText()
        {
            GrandLetterFirstValidationRule rule =
                new GrandLetterFirstValidationRule("Imię");
            ValidationResult result = rule.Validate("", null);
            Assert.AreEqual(new ValidationResult(false, rule.failure_message), result);
        }

        [TestMethod]
        public void ValueIsNull()
        {
            GrandLetterFirstValidationRule rule =
                new GrandLetterFirstValidationRule("Imię");
            ValidationResult result = rule.Validate(null, null);
            Assert.AreEqual(new ValidationResult(false, rule.failure_message), result);
        }
    }
}
