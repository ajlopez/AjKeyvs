namespace AjKeyvs.Server.Tests.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using AjKeyvs.Server.Parser;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandReaderTests
    {
        [TestMethod]
        public void ParseGetCommand()
        {
            CommandReader reader = new CommandReader("get users:1:name");

            CommandInfo command = reader.NextCommand();

            Assert.IsNotNull(command);
            Assert.AreEqual("get", command.Verb);
            Assert.AreEqual("users:1:name", command.Key);
            Assert.IsNull(command.Parameters);
        }

        [TestMethod]
        public void ParseSetCommand()
        {
            CommandReader reader = new CommandReader("set users:1:name \"Adam\"");

            CommandInfo command = reader.NextCommand();

            Assert.IsNotNull(command);
            Assert.AreEqual("set", command.Verb);
            Assert.AreEqual("users:1:name", command.Key);
            Assert.IsNotNull(command.Parameters);
            Assert.AreEqual(1, command.Parameters.Count);
            Assert.AreEqual("Adam", command.Parameters[0]);
        }

        [TestMethod]
        public void ParseNullCommand()
        {
            CommandReader reader = new CommandReader(string.Empty);

            CommandInfo command = reader.NextCommand();

            Assert.IsNull(command);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "Invalid Verb")]
        public void RaiseWhenVerbIsNotAName()
        {
            CommandReader reader = new CommandReader("1");

            CommandInfo command = reader.NextCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "Invalid Key")]
        public void RaiseWhenKeyIsNotAName()
        {
            CommandReader reader = new CommandReader("get 1");

            CommandInfo command = reader.NextCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "No Key in Command")]
        public void RaiseWhenNoKey()
        {
            CommandReader reader = new CommandReader("get");

            CommandInfo command = reader.NextCommand();
        }

        [TestMethod]
        public void LineReaderParseOnlyOneCommandInFirstLine()
        {
            CommandReader reader = new CommandReader(new StringReader("get key1\r\nget key2"), true);

            CommandInfo command = reader.NextCommand();
            Assert.IsNotNull(command);

            Assert.IsNull(reader.NextCommand());
        }
    }
}
