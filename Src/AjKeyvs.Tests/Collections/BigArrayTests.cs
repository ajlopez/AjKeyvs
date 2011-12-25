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
        public void GetDefaultOnEmptyArray()
        {
            BigArray<int> array = new BigArray<int>(10);

            for (int k = 0; k < 20; k++)
                Assert.AreEqual(0, array[(ulong)k]);
        }

        [TestMethod]
        public void GetDefaultOnUndefinedValuesOneNodeLevel()
        {
            BigArray<int> array = new BigArray<int>(10);

            for (int k = 0; k < 5; k++)
                array[(ulong)k] = k;

            for (int k = 100; k < 120; k++)
                Assert.AreEqual(0, array[(ulong)k]);
        }

        [TestMethod]
        public void GetDefaultOnUndefinedValuesTwoNodeLevels()
        {
            BigArray<int> array = new BigArray<int>(10);

            for (int k = 0; k < 20; k++)
                array[(ulong) k] = k;

            for (int k = 100; k < 120; k++)
                Assert.AreEqual(0, array[(ulong)k]);
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

        [TestMethod]
        public void AddLastAndFirstSixteenElementsNodeSizeSixteen()
        {
            BigArray<int> array = new BigArray<int>(16);

            for (int k = 0; k < 16; k++)
                array[(ulong)(ulong.MaxValue - (uint)k)] = k;
            for (int k = 0; k < 16; k++)
                array[(ulong)k] = k;

            for (int k = 0; k < 16; k++)
                Assert.AreEqual(k, array[(ulong)(ulong.MaxValue - (uint)k)]);

            for (int k = 0; k < 16; k++)
                Assert.AreEqual(k, array[(ulong) k]);
        }

        [TestMethod]
        public void MiddleElementsAreDefault()
        {
            BigArray<int> array = new BigArray<int>(16);

            for (int k = 0; k < 16; k++)
                array[(ulong)(ulong.MaxValue - (uint)k)] = k;
            for (int k = 0; k < 16; k++)
                array[(ulong)k] = k;

            for (ulong k = long.MaxValue - 16; k < long.MaxValue; k++)
                Assert.AreEqual(0, array[k]);
        }
    }
}
