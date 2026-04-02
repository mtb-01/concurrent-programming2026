using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.Logic.Test
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
            Vector vec = new(xComponent, yComponent);
            Assert.AreEqual<double>(xComponent, vec.X);
            Assert.AreEqual<double>(yComponent, vec.Y);
        }

        [TestMethod]
        public void EqualsTestMethod()
        {
            Random randomGenerator = new();
            double xComponent = randomGenerator.NextDouble();
            double yComponent = randomGenerator.NextDouble();
            Vector vec1 = new(xComponent, yComponent);
            Vector vec2 = new(xComponent, yComponent);
            
            Assert.IsTrue(vec1.Equals(vec2));

            int hash1 = vec1.GetHashCode();
            int hash2 = vec2.GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }
    }
}
