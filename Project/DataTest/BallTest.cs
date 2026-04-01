using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.Data.Test
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            Vector vector = new Vector(0.0, 0.0);
            double mass = 10;
            double circumference = 10;

            Ball ball = new Ball(vector, vector, mass, circumference);

            Assert.AreEqual<IVector>(vector, ball.Position);
            Assert.AreEqual<IVector>(vector, ball.Velocity);
            Assert.AreEqual<double>(mass, ball.Mass);
            Assert.AreEqual<double>(mass, ball.Circumference);
        }
    }
}
