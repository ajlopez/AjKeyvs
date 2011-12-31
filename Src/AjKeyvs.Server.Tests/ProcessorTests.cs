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
            processor.Process();
            Assert.AreEqual(1ul, repository.GetValue("counter"));
        }
    }
}
