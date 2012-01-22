using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AjKeyvs.Server.Tests
{
    [TestClass]
    public class SessionTests
    {
        private Repository repository;

        [TestInitialize]
        public void Setup()
        {
            this.repository = new Repository();
            this.repository.SetValue("one", 1);
            this.repository.SetValue("two", 2);
            this.repository.SetValue("name", "Adam");
        }

        [TestMethod]
        public void GetNullValue()
        {
            var result = this.RunSession("get foo\r\n");
            Assert.AreEqual("null\r\n", result);
        }

        [TestMethod]
        public void GetSimpleValue()
        {
            var result = this.RunSession("get one\r\n");
            Assert.AreEqual("1\r\n", result);
        }

        [TestMethod]
        public void GetTwoSimpleValues()
        {
            var result = this.RunSession("get one\r\nget two\r\n");
            Assert.AreEqual("1\r\n2\r\n", result);
        }

        [TestMethod]
        public void GetSimpleStringValue()
        {
            var result = this.RunSession("get name\r\n");
            Assert.AreEqual("\"Adam\"\r\n", result);
        }

        [TestMethod]
        public void SetSimpleValue()
        {
            var result = this.RunSession("set three 3\r\n");
            Assert.AreEqual("OK\r\n", result);
        }

        private string RunSession(string text)
        {
            StringReader reader = new StringReader(text);
            StringWriter writer = new StringWriter();
            Session session = new Session(this.repository, reader, writer);
            session.Process();
            writer.Close();
            return writer.ToString();
        }
    }
}
