using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjKeyvs.Collections;

namespace AjKeyvs.Tests.Collections
{
    [TestClass]
    public class HierarchicalDictionaryTests
    {
        private HierarchicalDictionary<string> dictionary;

        [TestInitialize]
        public void Setup()
        {
            this.dictionary = new HierarchicalDictionary<string>();
        }

        [TestMethod]
        public void GetSimpleKeyUndefinedValue()
        {
            Assert.IsNull(this.dictionary["foo"]);
        }

        [TestMethod]
        public void GetCompositeKeyUndefinedValue()
        {
            Assert.IsNull(this.dictionary["users:1"]);
        }
    }
}
