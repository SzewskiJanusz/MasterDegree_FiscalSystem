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
    public class OnlyNumbersAndLettersValidationRuleTest
    {
        public OnlyNumbersAndLettersValidationRuleTest()
        { }

        [TestMethod]
        public void HasDigitsAndLowerCaseLetters()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("24edsa", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasDigitsAndUpperCaseLetters()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("24ELOCOTAM", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasDigitsAndUpperAndLowerCaseLetters()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("24EloCotam", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasOnlyLetters()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("Pococikapusta", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasOnlyDigits()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("321312321", null);
            Assert.AreEqual(new ValidationResult(true, null), result);
        }

        [TestMethod]
        public void HasNoValue()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("", null);
            Assert.AreEqual(new ValidationResult(false, "Wpisz " + obj.PropertyName), result);
        }

        [TestMethod]
        public void ValueIsNull()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate(null, null);
            Assert.AreEqual(new ValidationResult(false, "Wpisz " + obj.PropertyName), result);
        }

        [TestMethod]
        public void HasSpecialChars()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("?!@#Sda", null);
            Assert.AreEqual(new ValidationResult(false, obj.PropertyName + " może się składać tylko z dużych liter i cyfr"), result);
        }

        [TestMethod]
        public void HasUnderScoreChars()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("12321_dDw1_", null);
            Assert.AreEqual(new ValidationResult(false, obj.PropertyName + " może się składać tylko z dużych liter i cyfr"), result);
        }

        [TestMethod]
        public void HasDashes()
        {
            OnlyNumbersAndLettersValidationRule obj = new OnlyNumbersAndLettersValidationRule();
            ValidationResult result = obj.Validate("75-900", null);
            Assert.AreEqual(new ValidationResult(false, obj.PropertyName + " może się składać tylko z dużych liter i cyfr"), result);
        }



    }
}
