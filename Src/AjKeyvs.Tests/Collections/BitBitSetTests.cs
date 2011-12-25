using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjKeyvs.Collections;

namespace AjKeyvs.Tests.Collections
{
    [TestClass]
    public class BitBitSetTests
    {
        [TestMethod]
        public void GetZeroOnEmptySet()
        {
            BigBitSet set = new BigBitSet();

            Assert.IsFalse(set[0]);
            Assert.IsFalse(set[1]);
            Assert.IsFalse(set[ulong.MaxValue]);
        }

        [TestMethod]
        public void SetAndGetFirstOnes()
        {
            BigBitSet set = new BigBitSet();

            for (uint k = 0; k < 16; k++)
                set[k] = true;

            for (uint k = 0; k < 16; k++)
                Assert.IsTrue(set[k]);
        }

        [TestMethod]
        public void SetAndGetLastOnes()
        {
            BigBitSet set = new BigBitSet();

            for (uint k = 0; k < 16; k++)
                set[ulong.MaxValue - k] = true;

            for (uint k = 0; k < 16; k++)
                Assert.IsTrue(set[ulong.MaxValue - k]);
        }

        [TestMethod]
        public void SetAndGetFirstAndLastOnes()
        {
            BigBitSet set = new BigBitSet();

            for (uint k = 0; k < 16; k++) 
                set[ulong.MaxValue - k] = true;

            for (uint k = 0; k < 16; k++)
                set[k] = true;

            for (uint k = 0; k < 16; k++)
                Assert.IsTrue(set[ulong.MaxValue - k]);

            for (uint k = 0; k < 16; k++)
                Assert.IsTrue(set[k]);
        }

        [TestMethod]
        public void SetOnesAndGetMiddleZeroes()
        {
            BigBitSet set = new BigBitSet();

            for (uint k = 0; k < 16; k++)
                set[ulong.MaxValue - k] = true;
            for (uint k = 0; k < 16; k++)
                set[k] = true;

            for (uint k = 0; k < 256; k++)
                Assert.IsFalse(set[(ulong) (long.MaxValue - k)]);
        }
    }
}
