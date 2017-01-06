﻿using FormulasCollection.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ExpressionIsAlwaysNull

namespace UnitTest.FormulasCollectionTests.HelpersTests
{
    [TestClass]
    public class SportConverterTypesTests
    {
        [TestMethod]
        public void MinimalizeValueTest_TestWithZero()
        {
            var inValue = "F1(+1.0)";
            var outValue = "F1(+1)";
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);

            inValue = "F1(-1.50)";
            outValue = "F1(-1.5)";
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);

            inValue = "F1(0)";
            outValue = "F1(0)";
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);

            inValue = "F2(0.25)";
            outValue = "F2(0.25)";
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);

            inValue = "F2(-3.0)";
            outValue = "F2(-3)";
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);
        }

        [TestMethod]
        public void MinimalizeValueTest_TestWithoutZero()
        {
            var inValue = "F1(+3.5)";
            var outValue = "F1(+3.5)";
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);

            inValue = "F1(+3.75)";
            outValue = "F1(+3.75)";
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);

            inValue = "F2(-1.75)";
            outValue = "F2(-1.75)";
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);

            inValue = string.Empty;
            outValue = string.Empty;
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);

            inValue = null;
            outValue = null;
            Assert.AreEqual(inValue.MinimalizeValue(), outValue);
        }
    }
}