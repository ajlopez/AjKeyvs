using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjKeyvs.Server.Tests
{
    [TestClass]
    public class ProcessorTests
    {
        private Repository repository;

        [TestInitialize]
        public void Setup()
        {
            this.repository = new Repository();
        }

        [TestMethod]
        public void ProcessSetKeyValue()
        {
            Processor processor = new Processor(this.repository, "set counter 1");
            Assert.IsNull(processor.Process());
            Assert.AreEqual(1ul, repository.GetValue("counter"));
        }

        [TestMethod]
        public void ProcessTwoSetKeyValues()
        {
            Processor processor = new Processor(this.repository, "set users:1:age 800\nset users:1:height 180");
            Assert.IsNull(processor.Process());
            Assert.IsNull(processor.Process());
            Assert.AreEqual(800ul, repository.GetValue("users:1:age"));
            Assert.AreEqual(180ul, repository.GetValue("users:1:height"));
        }
    }
}
