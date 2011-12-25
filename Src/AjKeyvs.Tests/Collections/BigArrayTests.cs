using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AjKeyvs.Collections;

namespace AjKeyvs.Tests.Collections
{
    [TestClass]
    public class BigArrayTests
    {
        [TestMethod]
        public void AddElementOne()
        {
            BigArray<int> array = new BigArray<int>();

            array[1] = 1;

            Assert.AreEqual(1, array[1]);
        }
    }
}
