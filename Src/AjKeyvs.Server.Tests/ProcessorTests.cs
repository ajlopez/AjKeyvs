using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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

        [TestMethod]
        public void ProcessSetAndGetKeyValue()
        {
            Processor processor = new Processor(this.repository, "set counter 1\nget counter");
            Assert.IsNull(processor.Process());
            Assert.AreEqual(1ul, processor.Process());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenSetCommandHasNoValue()
        {
            Processor processor = new Processor(this.repository, "set counter");
            processor.Process();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenGetCommandHasParameter()
        {
            Processor processor = new Processor(this.repository, "get counter 1");
            processor.Process();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenCommandHasNoKey()
        {
            Processor processor = new Processor(this.repository, "get");
            processor.Process();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenCommandHasAKeyThatIsNotAName()
        {
            Processor processor = new Processor(this.repository, "get 1");
            processor.Process();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenCommandHasAVerbThatIsNotAName()
        {
            Processor processor = new Processor(this.repository, "1 2 3");
            processor.Process();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenSetCommandHasManyValues()
        {
            Processor processor = new Processor(this.repository, "set counter 1 2");
            processor.Process();
        }

        [TestMethod]
        public void SetAndGetOneThousandUsers()
        {
            for (int k = 1; k <= 1000; k++)
            {
                Processor processor = new Processor(this.repository, string.Format("set users:{0}:age {0}\nset users:{0}:id {0}", k));
                Assert.IsNull(processor.Process());
                Assert.IsNull(processor.Process());
            }

            for (int k = 1; k <= 1000; k++)
            {
                Processor processor = new Processor(this.repository, string.Format("get users:{0}:age\nget users:{0}:id", k));
                Assert.AreEqual((ulong) k, processor.Process());
                Assert.AreEqual((ulong)k, processor.Process());
            }
        }
    }
}
