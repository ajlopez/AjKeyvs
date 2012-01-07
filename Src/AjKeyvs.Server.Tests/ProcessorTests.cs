using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using AjKeyvs.Collections;

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
            Assert.IsFalse(processor.ProcessCommand().HasValue);
            Assert.AreEqual(1ul, repository.GetValue("counter"));
        }

        [TestMethod]
        public void ProcessTwoSetKeyValues()
        {
            Processor processor = new Processor(this.repository, "set users:1:age 800\nset users:1:height 180");
            Assert.IsFalse(processor.ProcessCommand().HasValue);
            Assert.IsFalse(processor.ProcessCommand().HasValue);
            Assert.AreEqual(800ul, repository.GetValue("users:1:age"));
            Assert.AreEqual(180ul, repository.GetValue("users:1:height"));
        }

        [TestMethod]
        public void ProcessSetAndGetKeyValue()
        {
            Processor processor = new Processor(this.repository, "set counter 1\nget counter");
            Assert.IsFalse(processor.ProcessCommand().HasValue);
            Assert.AreEqual(1ul, processor.ProcessCommand().Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenSetCommandHasNoValue()
        {
            Processor processor = new Processor(this.repository, "set counter");
            processor.ProcessCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenGetCommandHasParameter()
        {
            Processor processor = new Processor(this.repository, "get counter 1");
            processor.ProcessCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenCommandHasNoKey()
        {
            Processor processor = new Processor(this.repository, "get");
            processor.ProcessCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenCommandHasAKeyThatIsNotAName()
        {
            Processor processor = new Processor(this.repository, "get 1");
            processor.ProcessCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenCommandHasAVerbThatIsNotAName()
        {
            Processor processor = new Processor(this.repository, "1 2 3");
            processor.ProcessCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaiseWhenSetCommandHasManyValues()
        {
            Processor processor = new Processor(this.repository, "set counter 1 2");
            processor.ProcessCommand();
        }

        [TestMethod]
        public void SetAndGetOneThousandUsers()
        {
            for (int k = 1; k <= 1000; k++)
            {
                Processor processor = new Processor(this.repository, string.Format("set users:{0}:age {0}\nset users:{0}:id {0}", k));
                Assert.IsFalse(processor.ProcessCommand().HasValue);
                Assert.IsFalse(processor.ProcessCommand().HasValue);
            }

            for (int k = 1; k <= 1000; k++)
            {
                Processor processor = new Processor(this.repository, string.Format("get users:{0}:age\nget users:{0}:id", k));
                Assert.AreEqual((ulong) k, processor.ProcessCommand().Value);
                Assert.AreEqual((ulong)k, processor.ProcessCommand().Value);
            }
        }

        [TestMethod]
        public void SetAddAndIsMember()
        {
            string command = string.Format("sadd users:1:followers {0}\r\nsismember users:1:followers {0}", 1);
            Processor processor = new Processor(this.repository, new StringReader(command));
            processor.ProcessCommand();
            CommandResult result = processor.ProcessCommand();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(true, result.Value);
        }

        [TestMethod]
        public void SetAddRemoveAndIsMember()
        {
            string command = string.Format("sadd users:1:followers {0}\r\nsrem users:1:followers {0}\r\nsismember users:1:followers {0}", 1);
            Processor processor = new Processor(this.repository, new StringReader(command));
            processor.ProcessCommand();
            processor.ProcessCommand();
            CommandResult result = processor.ProcessCommand();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(false, result.Value);
        }

        [TestMethod]
        public void SetIsMemberOnEmptySet()
        {
            for (int k = 1; k <= 1000; k++)
            {
                string command = string.Format("sismember users:1:followers {0}", 1);
                Processor processor = new Processor(this.repository, new StringReader(command));
                CommandResult result = processor.ProcessCommand();

                Assert.IsNotNull(result);
                Assert.IsTrue(result.HasValue);
                Assert.AreEqual(false, result.Value);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaisesIfUnknownVerb()
        {
            Processor processor = new Processor(this.repository, "foo key value");
            CommandResult result = processor.ProcessCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RaisesIfInvalidArity()
        {
            Processor processor = new Processor(this.repository, "set users:1:name");
            CommandResult result = processor.ProcessCommand();
        }
    }
}
