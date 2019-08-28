using Fiscal_Management_System.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiscal_Management_System_UnitTests
{
    [TestClass]
    public class DbInitForTests
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            using (var db = new FiscalDbContext())
            {
                if (db.Database.Exists())
                    db.Database.Delete();

                db.Database.Create();
            }

        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            using (var db = new FiscalDbContext())
                if (db.Database.Exists())
                    db.Database.Delete();
        }
    }
}
