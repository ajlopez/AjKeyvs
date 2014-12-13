namespace AjKeyvs.Tests.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjKeyvs.Collections;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void SetAndGetSimpleIntegerKeyValue()
        {
            this.dictionary["1"] = "one";

            Assert.AreEqual("one", this.dictionary["1"]);
        }

        [TestMethod]
        public void SetAndGetOneThousandUsers()
        {
            for (int k = 1; k <= 1000; k++)
            {
                this.dictionary[string.Format("users:{0}:name", k)] = string.Format("user{0}", k);
                this.dictionary[string.Format("users:{0}:id", k)] = k;
            }

            for (int k = 1; k <= 1000; k++)
            {
                Assert.AreEqual(string.Format("user{0}", k), this.dictionary[string.Format("users:{0}:name", k)]);
                Assert.AreEqual(k, this.dictionary[string.Format("users:{0}:id", k)]);
            }
        }
    }
}
