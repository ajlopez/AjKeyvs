using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjKeyvs.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private Repository repository;

        [TestInitialize]
        public void Setup()
        {
            this.repository = new Repository();
        }

        [TestMethod]
        public void GetUndefinedValue()
        {
            Assert.IsNull(repository.GetValue("foo"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            repository.SetValue("one", 1);
            Assert.AreEqual(1, repository.GetValue("one"));
        }

        [TestMethod]
        public void SetAndGetOneThousandUsers()
        {
            for (int k = 1; k <= 1000; k++)
            {
                this.repository.SetValue(string.Format("users:{0}:name", k), string.Format("user{0}", k));
                this.repository.SetValue(string.Format("users:{0}:id", k), k);
            }

            for (int k = 1; k <= 1000; k++)
            {
                Assert.AreEqual(string.Format("user{0}", k), this.repository.GetValue(string.Format("users:{0}:name", k)));
                Assert.AreEqual(k, this.repository.GetValue(string.Format("users:{0}:id", k)));
            }
        }
    }
}
