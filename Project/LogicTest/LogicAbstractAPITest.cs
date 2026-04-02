using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDataBall = Project.Data.IBall;
using IDataVector = Project.Data.IVector;
using Project.Data;

namespace Project.Logic.Test
{
    [TestClass]
    public class DataVector : IDataVector
    {
        public double X { get; init; }
        public double Y { get; init; }
        public DataVector(double xComponent, double yComponent)
        {
            X = xComponent;
            Y = yComponent;
        }
    }

    [TestClass]
    public class DataBall : IDataBall
    {
        public Data.IVector Position { get; set; }
        public Data.IVector Velocity { get; }
        public double Mass { get; init; }
        public double Circumference { get; init; }
        public DataBall(IDataVector initialPosition, IDataVector initialVelocity, double mass, double circumference)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
            Mass = mass;
            Circumference = circumference;
        }
    }

    [TestClass]
    public class TestDataImplementation : DataAbstractAPI
    {
        required public int NumberOfBalls { get; set; }
        required public IDataVector XPositionRange { get; set; }
        required public IDataVector YPositionRange { get; set; }
        required public IDataVector XVelocityRange { get; set; }
        required public IDataVector YVelocityRange { get; set; }
        required public IDataVector MassRange { get; set; }
        required public IDataVector CircumferenceRange { get; set; }
        public TestDataImplementation() { }
        private List<IDataBall> listOfBalls = new List<IDataBall>();

        public override List<IDataBall> GetBalls()
        {
            return new List<IDataBall>(listOfBalls);
        }
        private double GetRandomInRange(double rangeStart, double rangeEnd)
        {
            Random random = new Random();
            return rangeStart + (rangeEnd - rangeStart) * random.NextDouble();
        }
        public override void Load()
        {
            for (int i = 0; i < NumberOfBalls; i++)
            {
                double valuePosX = GetRandomInRange(XPositionRange.X, XPositionRange.Y);
                double valuePosY = GetRandomInRange(YPositionRange.X, YPositionRange.Y);
                double valueVelX = GetRandomInRange(XVelocityRange.X, XVelocityRange.Y);
                double valueVelY = GetRandomInRange(YVelocityRange.X, YVelocityRange.Y);
                double mass = GetRandomInRange(MassRange.X, MassRange.Y);
                double cir = GetRandomInRange(CircumferenceRange.X, CircumferenceRange.Y);

                DataVector pos = new(valuePosX, valuePosY);
                DataVector vel = new(valueVelX, valueVelY);

                AddBall(pos, vel, mass, cir);
            }
        }
        public override void ClearBalls() { }

        public override void Save() { }

        public override void AddBall(Data.IVector initialPosition, Data.IVector initialVelocity, double mass, double circumference)
        {
            DataBall ball = new(initialPosition, initialVelocity, mass, circumference);
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
            IDataVector range = new DataVector(1.0, 10.0);

            TestDataImplementation data = new TestDataImplementation()
            {
                NumberOfBalls = balls,
                XPositionRange = range,
                YPositionRange = range,
                XVelocityRange = range,
                YVelocityRange = range,
                MassRange = range,
                CircumferenceRange = range
            };

            LogicImplementation logic = new LogicImplementation(data);
            double delay = 10;
            logic.Start(delay);

            Assert.AreEqual(balls, logic.GetBalls().Count);
        }
    }
}