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
    }
}
