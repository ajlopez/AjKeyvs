using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjKeyvs.Server.Tests
{
    [TestClass]
    public class CommandInfoTests
    {
        [TestMethod]
        public void CreateCommandWithoutParameters()
        {
            CommandInfo info = new CommandInfo("get", "users:1:name", null);

            Assert.AreEqual("get", info.Verb);
            Assert.AreEqual("users:1:name", info.Key);
            Assert.IsNull(info.Parameters);
        }

        [TestMethod]
        public void CreateCommandWithParameters()
        {
            CommandInfo info = new CommandInfo("set", "users:1:id", new object[] { 1ul });

            Assert.AreEqual("set", info.Verb);
            Assert.AreEqual("users:1:id", info.Key);
            Assert.IsNotNull(info.Parameters);
            Assert.AreEqual(1, info.Parameters.Count);
            Assert.AreEqual(1ul, info.Parameters[0]);
        }
    }
}
