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
        public TestDataImplementation() { }
        private List<IDataBall> listOfBalls = new List<IDataBall>();

        public override List<IDataBall> GetBalls()
        {
            return new List<IDataBall>(listOfBalls);
        }
        public override void Load(int count = 3)
        {
            for (int i = 0; i < count; i++)
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
            RaiseBallAddedNotification(ball);
        }
    }

    [TestClass]
    public class LogicAbstractAPITest
    {
        [TestMethod]
        public void StartTestMethod()
        {
            int balls = 3;

            TestDataImplementation data = new TestDataImplementation();

            LogicImplementation logic = new LogicImplementation(100, 100, data);
            logic.StartLayer(balls);

            Assert.AreEqual(balls, logic.GetBalls().Count);
            Assert.IsFalse(logic.IsStarted());

            double delay = 10;
            logic.Start(delay);
            Assert.IsTrue(logic.IsStarted());
            
            logic.Stop();
            Assert.IsFalse(logic.IsStarted());
        }

        [TestMethod]
        public void ConstructorTestMethod()
        {
            int balls = 3;
            TestDataImplementation data = new TestDataImplementation();
            double X = 10;
            double Y = 10;
            Vector size = new Vector(10, 10);
            ICollisionObject area = new Area(X, Y);
            LogicImplementation logic = new LogicImplementation(X, Y, data);
            logic.StartLayer(balls);

            Assert.AreEqual(size, logic.GetAreaSize());
        }

        [TestMethod]
        public void BallOperationsTestMethod()
        {
            int balls = 3;
            TestDataImplementation data = new TestDataImplementation();
            double X = 10;
            double Y = 10;
            LogicImplementation logic = new LogicImplementation(X, Y, data);
            logic.StartLayer(balls);

            Assert.AreEqual(balls, logic.GetBalls().Count);
            logic.CreateBall();
            Assert.AreEqual(balls+1, logic.GetBalls().Count);

            logic.ClearBalls();
            Assert.AreEqual(0, logic.GetBalls().Count);
        }
    }
}