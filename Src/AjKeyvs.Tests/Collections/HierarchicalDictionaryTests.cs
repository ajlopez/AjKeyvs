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
        private HierarchicalDictionary<object> dictionary;

        [TestInitialize]
        public void Setup()
        {
            this.dictionary = new HierarchicalDictionary<object>();
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

        [TestMethod]
        public void SetAndGetSimpleKeyValue()
        {
            this.dictionary["one"] = 1;
            Assert.AreEqual(1, this.dictionary["one"]);
        }

        [TestMethod]
        public void SetAndGetCompositeKeyValue()
        {
            this.dictionary["users:1:name"] = "Adam";
            this.dictionary["users:1:age"] = 800;
    
            Assert.AreEqual(800, this.dictionary["users:1:age"]);
        }
    }
}
