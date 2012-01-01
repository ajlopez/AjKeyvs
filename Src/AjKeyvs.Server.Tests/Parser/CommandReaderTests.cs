using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjKeyvs.Server.Parser;

namespace AjKeyvs.Server.Tests.Parser
{
    [TestClass]
    public class CommandReaderTests
    {
        [TestMethod]
        public void ParseGetCommand()
        {
            CommandReader reader = new CommandReader("get users:1:name");

            Command command = reader.NextCommand();

            Assert.IsNotNull(command);
            Assert.AreEqual("get", command.Verb);
            Assert.AreEqual("users:1:name", command.Key);
            Assert.IsNull(command.Parameters);
        }

        [TestMethod]
        public void ParseNullCommand()
        {
            CommandReader reader = new CommandReader("");

            Command command = reader.NextCommand();

            Assert.IsNull(command);
        }
    }
}
