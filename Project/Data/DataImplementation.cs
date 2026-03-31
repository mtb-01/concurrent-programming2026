using System;
using System.Collections.Generic;

namespace Project.Data
{
    internal class DataImplementation : DataAbstractAPI
    {
        public int NumberOfBalls { get; init; }
        private List<IBall>? ListOfBalls { get; set; }
        internal DataImplementation (int numberOfBalls)
        {
            NumberOfBalls = numberOfBalls;
            ListOfBalls = null;
        }
        public override void AddBall(IVector initialPosition, IVector initialVelocity, double mass, double circumference)
        {
            Ball ball = new(initialPosition, initialVelocity, mass, circumference);
            ListOfBalls.Add(ball);
        }

        public override void ClearBalls()
        {
            // czysci liste
            ListOfBalls = null;
        }

        public override List<IBall> GetBalls()
        {
            // po prostu zwraca liste
            return ListOfBalls;
        }

        public override void Load()
        {
            // wypelnia liste losowymi kulkami
            for (int i = 0; i < NumberOfBalls; i++)
            {
                double valuePosX = 10.0 * new Random().NextDouble();
                double valuePosY = 10.0 * new Random().NextDouble();
                double valueVelX = 10.0 * new Random().NextDouble();
                double valueVelY = 10.0 * new Random().NextDouble();
                double valueMass = 0.1 + (10.0 - 0.1) * new Random().NextDouble();
                double valueCir = 0.1 + (10.0 - 0.1) * new Random().NextDouble();

                Vector pos = new(valuePosX, valuePosY);
                Vector vel = new(valueVelX, valueVelY);
                double mass = valueMass;
                double cir = valueCir;

                AddBall(pos, vel, mass, cir);
            }
        }

        public override void Save()
        {
            // ?
        }
    }
}