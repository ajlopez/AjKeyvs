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
        [TestMethod]
        public void GetUndefinedValue()
        {
            Repository repo = new Repository();

            Assert.IsNull(repo.GetValue("foo"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            Repository repo = new Repository();

            repo.SetValue("one", 1);
            Assert.AreEqual(1, repo.GetValue("one"));
        }
    }
}
