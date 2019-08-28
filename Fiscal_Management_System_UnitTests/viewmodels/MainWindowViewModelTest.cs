using Fiscal_Management_System.viewmodels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fiscal_Management_System_UnitTests.viewmodels
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        [TestMethod]
        public void Navigation_BackFromUserControlTest()
        {
            MainViewModel mvm = new MainViewModel();

            UserControl UC_A = new UserControl() {  Tag = "A" };
            UserControl UC_B = new UserControl() { Tag = "B" };

            mvm.SetUserControl(UC_A);
            mvm.SetUserControl(UC_B);
            mvm.SetLastUserControl(null);

            Assert.AreEqual("A", mvm.UserControl.Tag);
        }

        [TestMethod]
        public void Navigation_ForwardToUserControlTest()
        {
            MainViewModel mvm = new MainViewModel();

            UserControl UC_A = new UserControl() { Tag = "A" };
            UserControl UC_B = new UserControl() { Tag = "B" };
            UserControl UC_C = new UserControl() { Tag = "C" };
            UserControl UC_D = new UserControl() { Tag = "D" };

            mvm.SetUserControl(UC_A);
            mvm.SetUserControl(UC_B);
            mvm.SetUserControl(UC_C);
            mvm.SetUserControl(UC_D);
            // Now is D

            // Going to C
            mvm.SetLastUserControl(null);
            // Going to B
            mvm.SetLastUserControl(null);
            // Going to C
            mvm.SetNextUserControl(null);

            Assert.AreEqual("C", mvm.UserControl.Tag);
        }

        [TestMethod]
        public void Navigation_NoReactionWhenClickedBackTooManyTimesTest()
        {
            MainViewModel mvm = new MainViewModel();

            UserControl UC_A = new UserControl() { Tag = "A" };
            UserControl UC_B = new UserControl() { Tag = "B" };

            mvm.SetUserControl(UC_A);
            mvm.SetUserControl(UC_B);
            mvm.SetLastUserControl(null);
            mvm.SetLastUserControl(null);
            mvm.SetLastUserControl(null);
            mvm.SetLastUserControl(null);
            mvm.SetLastUserControl(null);

            Assert.AreEqual("Clients", mvm.UserControl.Tag);
        }

        [TestMethod]
        public void Navigation_NoReactionWhenClickedForwardTooManyTimesTest()
        {
            MainViewModel mvm = new MainViewModel();

            UserControl UC_A = new UserControl() { Tag = "A" };
            UserControl UC_B = new UserControl() { Tag = "B" };

            mvm.SetUserControl(UC_A);
            mvm.SetUserControl(UC_B);
            mvm.SetLastUserControl(null);
            mvm.SetLastUserControl(null);
            mvm.SetLastUserControl(null);
            mvm.SetLastUserControl(null);
            mvm.SetLastUserControl(null);
            mvm.SetNextUserControl(null);
            mvm.SetNextUserControl(null);
            mvm.SetNextUserControl(null);
            mvm.SetNextUserControl(null);
            mvm.SetNextUserControl(null);
            mvm.SetNextUserControl(null);

            Assert.AreEqual("B", mvm.UserControl.Tag);
        }

        [TestMethod]
        public void ButtonCommandForwardTest()
        {
            MainViewModel mvm = new MainViewModel();

            UserControl UC_A = new UserControl() { Tag = "A" };
            UserControl UC_B = new UserControl() { Tag = "B" };

            mvm.SetUserControl(UC_A);
            mvm.SetUserControl(UC_B);

            mvm.SetLastUserControl(null);

            ICommand a = mvm.SetNextUserControlButtonCommand;
            a.Execute(null);

            Assert.AreEqual("B", mvm.UserControl.Tag);
        }

        [TestMethod]
        public void ButtonCommandBackTest()
        {
            MainViewModel mvm = new MainViewModel();

            UserControl UC_A = new UserControl() { Tag = "A" };
            UserControl UC_B = new UserControl() { Tag = "B" };

            mvm.SetUserControl(UC_A);
            mvm.SetUserControl(UC_B);

            ICommand a = mvm.SetPreviousUserControlButtonCommand;
            a.Execute(null);

            Assert.AreEqual("A", mvm.UserControl.Tag);
        }
    }
}
