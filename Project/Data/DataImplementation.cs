using System;
using System.Collections.Generic;

namespace Project.Data
{
    internal class DataImplementation : DataAbstractAPI
    {
        required public IVector XPositionRange { get; set; }
        required public IVector YPositionRange { get; set; }
        required public IVector XVelocityRange { get; set; }
        required public IVector YVelocityRange { get; set; }
        required public IVector MassRange { get; set; }
        required public IVector CircumferenceRange { get; set; }

        private readonly List<IBall> listOfBalls = new List<IBall>();

        public DataImplementation () {}

        public override void AddBall(IVector initialPosition, IVector initialVelocity, double mass, double circumference)
        {
            Ball ball = new(initialPosition, initialVelocity, mass, circumference);
            listOfBalls.Add(ball);
            RaiseBallAddedNotification(ball);
        }

        public override void ClearBalls()
        {
            listOfBalls.Clear();
        }

        public override List<IBall> GetBalls()
        {
            return new List<IBall>(listOfBalls);
        }

        private double GetRandomInRange(double rangeStart, double rangeEnd)
        {
            Random random = new Random();
            return rangeStart + (rangeEnd - rangeStart) * random.NextDouble();
        }

        public override void Load(int count)
        {
            for (int i = 0; i < count; i++)
            {
                double valuePosX = GetRandomInRange(XPositionRange.X, XPositionRange.Y);
                double valuePosY = GetRandomInRange(YPositionRange.X, YPositionRange.Y);
                double valueVelX = GetRandomInRange(XVelocityRange.X, XVelocityRange.Y);
                double valueVelY = GetRandomInRange(YVelocityRange.X, YVelocityRange.Y);
                double mass = GetRandomInRange(MassRange.X, MassRange.Y);
                double cir = GetRandomInRange(CircumferenceRange.X, CircumferenceRange.Y);

                Vector pos = new(valuePosX, valuePosY);
                Vector vel = new(valueVelX, valueVelY);

                AddBall(pos, vel, mass, cir);
            }
        }

        public override void Save() {}
    }
}