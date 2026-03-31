using System;
using System.Collections.Generic;

namespace Project.Data
{
    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI GetDataLayer(int numberOfBalls)
        {
            return new DataImplementation(numberOfBalls);
        }

        public abstract void Save();

        public abstract void Load();

        public abstract List<IBall> GetBalls();

        public abstract void ClearBalls();

        public abstract void AddBall(IVector initialPosition, IVector initialVelocity, double mass, double circumference);
    }

    public interface IVector
    {
        double X { get; init; }
        double Y { get; init; }
    }

    public interface IBall
    {
        IVector Position { get; set; }
        IVector Velocity { get; }
        double Mass { get; init; }
        double Circumference { get; init; }
    }
}