using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.Data.Test
{
    [TestClass]
    public class VectorTest
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            Random randomGenerator = new();
            double xComponent = randomGenerator.NextDouble();
            double yComponent = randomGenerator.NextDouble();
            Vector newInstance = new(xComponent, yComponent);
            Assert.AreEqual<double>(xComponent, newInstance.X);
            Assert.AreEqual<double>(yComponent, newInstance.Y);
        }
    }
}
