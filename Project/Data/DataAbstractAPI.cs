using System;
using System.Collections.Generic;

namespace Project.Data
{
    public abstract class DataAbstractAPI
    {
        public abstract void Save();

        public abstract void Load();

        public abstract List<IBall> GetBalls();

        public abstract bool ClearBalls();

        public abstract bool AddBall(IVector initialPosition, IVector initialVelocity, double mass, double circumference);
    }

    public interface IVector
    {
        double x { get; init; }
        double y { get; init; }
    }

    public interface IBall
    {
        IVector Position { get; set; }
        IVector Velocity { get; }
        double Mass { get; init; }
        double Circumference { get; init; }
        event EventHandler<IVector> NewPositionNotification;
        public void Start(int ballCount)
        {
            throw new NotImplementedException();
        }
    }
}