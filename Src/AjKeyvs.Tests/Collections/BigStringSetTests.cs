namespace AjKeyvs.Tests.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjKeyvs.Collections;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void HasMemberOnEmptySet()
        {
            Assert.IsFalse(this.set.HasMember("foo"));
        }

        [TestMethod]
        public void RemoveOnEmptySet()
        {
            this.set.RemoveMember("foo");
            Assert.IsFalse(this.set.HasMember("foo"));
        }

        [TestMethod]
        public void RemoveMemberNotInSet()
        {
            this.set.AddMember("bar");
            this.set.RemoveMember("foo");
            Assert.IsFalse(this.set.HasMember("foo"));
            Assert.IsTrue(this.set.HasMember("bar"));
        }

        [TestMethod]
        public void SetAndGetString()
        {
            this.set.AddMember("foo");
            Assert.IsTrue(this.set.HasMember("foo"));
            Assert.IsFalse(this.set.HasMember("bar"));
        }

        [TestMethod]
        public void SetAndGetOneThousandStrings()
        {
            for (int k = 1; k <= 1000; k++)
                this.set.AddMember(string.Format("user{0}", k));

            for (int k = 1; k <= 1000; k++)
            {
                Assert.IsTrue(this.set.HasMember(string.Format("user{0}", k)));
                Assert.IsFalse(this.set.HasMember(string.Format("user{0}", k + 10000)));
            }
        }

        [TestMethod]
        public void SetRemoveAndGetOneThousandStrings()
        {
            for (int k = 1; k <= 1000; k++)
                this.set.AddMember(string.Format("user{0}", k));

            for (int k = 1; k <= 1000; k++)
                this.set.RemoveMember(string.Format("user{0}", k));

            for (int k = 1; k <= 1000; k++)
            {
                Assert.IsFalse(this.set.HasMember(string.Format("user{0}", k)));
                Assert.IsFalse(this.set.HasMember(string.Format("user{0}", k + 10000)));
            }
        }
    }
}
