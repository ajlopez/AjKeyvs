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

        [TestMethod]
        public void GetItemsFromEmptySet()
        {
            for (int k = 1; k <= 1000; k++)
                Assert.IsFalse(this.repository.SetHasMember("users:1:followers", (ulong)k));
        }

        [TestMethod]
        public void GetStringItemsFromEmptySet()
        {
            for (int k = 1; k <= 1000; k++)
                Assert.IsFalse(this.repository.SetHasMember("users:1:followers", string.Format("user{0}", k)));
        }

        [TestMethod]
        public void SetAndGetItemsFromSet()
        {
            for (int k = 1; k <= 1000; k++)
                this.repository.SetAddMember("users:1:followers", (ulong) k);

            for (int k = 1; k <= 1000; k++) 
                Assert.IsTrue(this.repository.SetHasMember("users:1:followers", (ulong)k));
        }

        [TestMethod]
        public void SetAndGetStringItemsFromSet()
        {
            for (int k = 1; k <= 1000; k++)
                this.repository.SetAddMember("users:1:followers", string.Format("user{0}", k));

            for (int k = 1; k <= 1000; k++)
                Assert.IsTrue(this.repository.SetHasMember("users:1:followers", string.Format("user{0}",k)));
        }

        [TestMethod]
        public void SetRemoveAndGetItemsSet()
        {
            for (int k = 1; k <= 1000; k++)
                this.repository.SetAddMember("users:1:followers", (ulong)k);

            for (int k = 1; k <= 1000; k++)
                this.repository.SetRemoveMember("users:1:followers", (ulong)k);

            for (int k = 1; k <= 1000; k++)
                Assert.IsFalse(this.repository.SetHasMember("users:1:followers", (ulong)k));
        }

        [TestMethod]
        public void SetRemoveAndGetStringItemsSet()
        {
            for (int k = 1; k <= 1000; k++)
                this.repository.SetAddMember("users:1:followers", string.Format("user{0}",k));

            for (int k = 1; k <= 1000; k++)
                this.repository.SetRemoveMember("users:1:followers", string.Format("user{0}",k));

            for (int k = 1; k <= 1000; k++)
                Assert.IsFalse(this.repository.SetHasMember("users:1:followers", string.Format("user{0}",k)));
        }

        [TestMethod]
        public void RemoveAndGetItemsSet()
        {
            for (int k = 1; k <= 1000; k++)
                this.repository.SetRemoveMember("users:1:followers", (ulong)k);

            for (int k = 1; k <= 1000; k++)
                Assert.IsFalse(this.repository.SetHasMember("users:1:followers", (ulong)k));
        }

        [TestMethod]
        public void RemoveAndGetStringItemsSet()
        {
            for (int k = 1; k <= 1000; k++)
                this.repository.SetRemoveMember("users:1:followers", string.Format("user{0}",k));

            for (int k = 1; k <= 1000; k++)
                Assert.IsFalse(this.repository.SetHasMember("users:1:followers", string.Format("user{0}",k)));
        }
    }
}
