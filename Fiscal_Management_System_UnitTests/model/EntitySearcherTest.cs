using Fiscal_Management_System;
using Fiscal_Management_System.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiscal_Management_System_UnitTests.model
{
    [TestClass]
    public class EntitySearcherTest
    {
        public EntitySearcherTest() { }

        class TestModel
        {
            public int Age { get; set; }
            public string Name { get; set; }
        }

        [TestMethod]
        public void SearchInEmptyCollection()
        {
            EntitySearcher<TestModel> es = new EntitySearcher<TestModel>();
            ObservableCollection<TestModel> wholeCollection = new ObservableCollection<TestModel>() { };
            ObservableCollection<TestModel> oc = es.GetFilteredData(wholeCollection, "Zuzia");
            Assert.AreEqual(0, oc.Count);
        }

        [TestMethod]
        public void EmptySearchInCollection()
        {
            EntitySearcher<TestModel> es = new EntitySearcher<TestModel>();
            ObservableCollection<TestModel> wholeCollection = new ObservableCollection<TestModel>()
            {
                new TestModel(){Age = 29 , Name = "Jan Kowalski"},
                new TestModel() { Age = 15, Name = "Janina Krulczyk" },
                new TestModel() { Age = 1, Name = "Malwina Zapolska" },
                new TestModel() { Age = 68, Name = "Józef Nazarejski" }
            };
            ObservableCollection<TestModel> oc = es.GetFilteredData(wholeCollection, "");
            Assert.AreEqual("29Jan Kowalski 15Janina Krulczyk 1Malwina Zapolska 68Józef Nazarejski", 
                oc[0].Age + oc[0].Name + " " + oc[1].Age + oc[1].Name + " " + oc[2].Age + oc[2].Name + " " + oc[3].Age + oc[3].Name);
        }

        [TestMethod]
        public void SearchInCollection_One()
        {
            EntitySearcher<TestModel> es = new EntitySearcher<TestModel>();
            ObservableCollection<TestModel> wholeCollection = new ObservableCollection<TestModel>()
            {
                new TestModel(){Age = 29 , Name = "Jan Kowalski"},
                new TestModel() { Age = 15, Name = "Janina Krulczyk" },
                new TestModel() { Age = 1, Name = "Malwina Zapolska" },
                new TestModel() { Age = 68, Name = "Józef Nazarejski" }
            };
            ObservableCollection<TestModel> oc = es.GetFilteredData(wholeCollection, "Jan");
            Assert.AreEqual("29Jan Kowalski 15Janina Krulczyk", oc[0].Age + oc[0].Name + " " + oc[1].Age + oc[1].Name);
        }

    }
}
