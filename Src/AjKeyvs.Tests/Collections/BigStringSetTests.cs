using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjKeyvs.Collections;

namespace AjKeyvs.Tests.Collections
{
    [TestClass]
    public class BigStringSetTests
    {
        private BigStringSet set;

        [TestInitialize]
        public void Setup()
        {
            this.set = new BigStringSet();
        }

        [TestMethod]
        public void GetStringOnEmptySet()
        {
            Assert.IsFalse(this.set["foo"]);
        }
    }
}
