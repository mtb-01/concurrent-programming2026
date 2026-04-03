using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDataBall = Project.Data.IBall;
using IDataVector = Project.Data.IVector;
using Project.Data;

namespace Project.Logic.Test
{
    internal class TestDataVector : IDataVector
    {
        public double X { get; init; }
        public double Y { get; init; }
        public TestDataVector(double xComponent, double yComponent)
        {
            X = xComponent;
            Y = yComponent;
        }
    }

    internal class TestDataBall : IDataBall
    {
        public IDataVector Position { get; set; }
        public IDataVector Velocity { get; }
        public double Mass { get; init; }
        public double Circumference { get; init; }
        public TestDataBall(IDataVector initialPosition, IDataVector initialVelocity, double mass, double circumference)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
            Mass = mass;
            Circumference = circumference;
        }
    }

    internal class TestDataImplementation : DataAbstractAPI
    {
        required public int NumberOfBalls { get; set; }
        public TestDataImplementation() { }
        private List<IDataBall> listOfBalls = new List<IDataBall>();

        public override List<IDataBall> GetBalls()
        {
            return new List<IDataBall>(listOfBalls);
        }
        public override void Load()
        {
            for (int i = 0; i < NumberOfBalls; i++)
            {
                double valuePosX = 10.0;
                double valuePosY = 10.0;
                double valueVelX = 10.0;
                double valueVelY = 10.0;
                double mass = 10.0;
                double cir = 10.0;

                TestDataVector pos = new(valuePosX, valuePosY);
                TestDataVector vel = new(valueVelX, valueVelY);

                AddBall(pos, vel, mass, cir);
            }
        }
        public override void ClearBalls() { }

        public override void Save() { }

        public override void AddBall(IDataVector initialPosition, IDataVector initialVelocity, double mass, double circumference)
        {
            TestDataBall ball = new(initialPosition, initialVelocity, mass, circumference);
            listOfBalls.Add(ball);
        }
    }

    [TestClass]
    public class LogicAbstractAPITest
    {
        [TestMethod]
        public void StartTestMethod()
        {
            int balls = 3;

            TestDataImplementation data = new TestDataImplementation()
            {
                NumberOfBalls = balls
            };

            LogicImplementation logic = new LogicImplementation(data);
            double delay = 10;
            logic.Start(delay);

            Assert.AreEqual(balls, logic.GetBalls().Count);
        }
    }
}