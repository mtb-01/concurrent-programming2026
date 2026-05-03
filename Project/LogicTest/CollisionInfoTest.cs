using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.Logic.Test
{
    [TestClass]
    public class CollisionInfoTest
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            bool collided = true;
            double fraction = 10;
            Vector velocity = new Vector(10, 10);
            Vector defaultVelocity = new Vector(0, 0);

            CollisionInfo infoNoArg = new CollisionInfo();

            Assert.AreEqual<bool>(false, infoNoArg.Collided);
            Assert.AreEqual<double>(1, infoNoArg.MoveFraction);
            Assert.AreEqual<Vector>(defaultVelocity, infoNoArg.NewVelocity);

            CollisionInfo infoArgs = new CollisionInfo(collided, fraction, velocity);

            Assert.AreEqual<bool>(collided, infoArgs.Collided);
            Assert.AreEqual<double>(fraction, infoArgs.MoveFraction);
            Assert.AreEqual<Vector>(velocity, infoArgs.NewVelocity);
        }
    }
}