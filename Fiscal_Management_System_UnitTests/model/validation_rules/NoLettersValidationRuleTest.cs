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
    public class NoLettersValidationRuleTest
    {
        public NoLettersValidationRuleTest() { }

        [TestMethod]
        public void HasDigitsAndDashes()
        {
            NoLettersValidationRule text_val = new NoLettersValidationRule();
            ValidationResult result = text_val.Validate("123-456-789", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasLetter()
        {
            NoLettersValidationRule text_val = new NoLettersValidationRule();
            ValidationResult result = text_val.Validate("tel. 123-456-789", null);
            Assert.AreEqual(new ValidationResult(false, text_val.PropertyName + " nie może zawierać liter"), result);
        }

        [TestMethod]
        public void HasDigitsDotsSpecialCharSpacesAndUnderscores()
        {
            NoLettersValidationRule text_val = new NoLettersValidationRule();
            ValidationResult result = text_val.Validate("123$, #94, 4 + 3 / 42. _@9#)~", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasQuestionMarks()
        {
            NoLettersValidationRule text_val = new NoLettersValidationRule();
            ValidationResult result = text_val.Validate("447???", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasExclMark()
        {
            NoLettersValidationRule text_val = new NoLettersValidationRule();
            ValidationResult result = text_val.Validate("9000!!", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void ValueIsNull()
        {
            NoLettersValidationRule text_val = new NoLettersValidationRule();
            ValidationResult result = text_val.Validate(null, null);
            Assert.AreEqual(new ValidationResult(false, "Wpisz wartość"), result);
        }

        [TestMethod]
        public void ValueIsEmpty()
        {
            NoLettersValidationRule text_val = new NoLettersValidationRule();
            ValidationResult result = text_val.Validate("", null);
            Assert.AreEqual(new ValidationResult(false, "Wpisz wartość"), result);
        }
    }
}
