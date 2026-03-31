using System;
using System.Collections.Generic;

namespace Project.Data
{
    internal class DataImplementation : DataAbstractAPI
    {
        public int NumberOfBalls { get; init; }
        private List<IBall> ListOfBalls { get; set; } = new List<IBall>();

        internal DataImplementation (int numberOfBalls)
        {
            NumberOfBalls = numberOfBalls;
        }

        public override void AddBall(IVector initialPosition, IVector initialVelocity, double mass, double circumference)
        {
            Ball ball = new(initialPosition, initialVelocity, mass, circumference);
            ListOfBalls.Add(ball);
        }

        public override void ClearBalls()
        {
            ListOfBalls.Clear();
        }

        public override List<IBall> GetBalls()
        {
            return ListOfBalls;
        }

        public override void Load()
        {
            for (int i = 0; i < NumberOfBalls; i++)
            {
                Random random = new Random();

                double valuePosX = 400.0 * random.NextDouble();
                double valuePosY = 400.0 * random.NextDouble();
                double valueVelX = 50.0 * (random.NextDouble() - 0.5);
                double valueVelY = 50.0 * (random.NextDouble() - 0.5);
                double mass = 0.1 + (10.0 - 0.1) * random.NextDouble();
                double cir = 0.1 + (10.0 - 0.1) * random.NextDouble();

                Vector pos = new(valuePosX, valuePosY);
                Vector vel = new(valueVelX, valueVelY);

                AddBall(pos, vel, mass, cir);
            }
        }

        public override void Save() {}
    }
}