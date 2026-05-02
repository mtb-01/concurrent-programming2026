using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.Data.Test
{
    [TestClass]
    public class DataAbstractAPITest
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            IVector xPositionRange = new Vector(-10.0, 10.0);
            IVector yPositionRange = new Vector(-10.0, 10.0);
            IVector xVelocityRange = new Vector(-10.0, 10.0);
            IVector yVelocityRange = new Vector(-10.0, 10.0);
            IVector massRange = new Vector(1.0, 10.0);
            IVector circumferenceRange = new Vector(1.0, 10.0);

            DataImplementation data = new DataImplementation()
            {
                XPositionRange = xPositionRange,
                YPositionRange = yPositionRange,
                XVelocityRange = xVelocityRange,
                YVelocityRange = yVelocityRange,
                MassRange = massRange,
                CircumferenceRange = circumferenceRange
            };

            Assert.AreEqual<IVector>(xPositionRange, data.XPositionRange);
            Assert.AreEqual<IVector>(yPositionRange, data.YPositionRange);
            Assert.AreEqual<IVector>(xVelocityRange, data.XVelocityRange);
            Assert.AreEqual<IVector>(yVelocityRange, data.YVelocityRange);
            Assert.AreEqual<IVector>(massRange, data.MassRange);
            Assert.AreEqual<IVector>(circumferenceRange, data.CircumferenceRange);
        }

        [TestMethod]
        public void ListOfBallsTestMethod()
        {
            int balls = 3;
            IVector range = new Vector(1.0, 10.0);

            DataImplementation data = new DataImplementation()
            {
                XPositionRange = range,
                YPositionRange = range,
                XVelocityRange = range,
                YVelocityRange = range,
                MassRange = range,
                CircumferenceRange = range
            };

            Assert.AreEqual(0, data.GetBalls().Count);

            Vector vector = new Vector(0.0, 0.0);
            double mass = 10;
            double circumference = 10;

            data.AddBall(vector, vector, mass, circumference);

            Assert.AreEqual(1, data.GetBalls().Count);

            data.ClearBalls();

            Assert.AreEqual(0, data.GetBalls().Count);

            data.Load(balls);

            Assert.AreEqual(balls, data.GetBalls().Count);
        }
    }
}