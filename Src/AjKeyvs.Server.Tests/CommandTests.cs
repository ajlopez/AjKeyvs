using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjKeyvs.Server.Tests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void CreateCommandWithoutParameters()
        {
            Command command = new Command("get", "users:1:name", null);

            Assert.AreEqual("get", command.Verb);
            Assert.AreEqual("users:1:name", command.Key);
            Assert.IsNull(command.Parameters);
        }

        [TestMethod]
        public void CreateCommandWithParameters()
        {
            Command command = new Command("set", "users:1:id", new object[] { 1ul });

            Assert.AreEqual("set", command.Verb);
            Assert.AreEqual("users:1:id", command.Key);
            Assert.IsNotNull(command.Parameters);
            Assert.AreEqual(1, command.Parameters.Count);
            Assert.AreEqual(1ul, command.Parameters[0]);
        }
    }
}
