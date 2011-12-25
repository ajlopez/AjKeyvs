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

        [TestMethod]
        public void AddFirstTenElementsNodeSizeTen()
        {
            BigArray<int> array = new BigArray<int>(10);

            for (int k = 0; k < 10; k++)
                array[(ulong) k] = k;

            for (int k = 0; k < 10; k++)
                Assert.AreEqual(k, array[(ulong) k]);
        }

        [TestMethod]
        public void AddFirstTwentyElementsNodeSizeTen()
        {
            BigArray<int> array = new BigArray<int>(10);

            for (int k = 0; k < 20; k++)
                array[(ulong)k] = k;

            for (int k = 0; k < 20; k++)
                Assert.AreEqual(k, array[(ulong)k]);
        }

        [TestMethod]
        public void AddLastSixteenElementsNodeSizeSixteen()
        {
            BigArray<int> array = new BigArray<int>(16);

            for (int k = 0; k < 16; k++)
                array[(ulong)(ulong.MaxValue - (uint) k)] = k;

            for (int k = 0; k < 16; k++)
                Assert.AreEqual(k, array[(ulong)(ulong.MaxValue - (uint) k)]);
        }
    }
}
