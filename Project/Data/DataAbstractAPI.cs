using System;
using System.Collections.Generic;

namespace Project.Data
{
    public abstract class DataAbstractAPI
    {
        private static DataAbstractAPI? dataLayer;

        public static void SetDataLayer(DataAbstractAPI dataLayer)
        {
            DataAbstractAPI.dataLayer = dataLayer;
        }

        public static DataAbstractAPI GetDataLayer()
        {
            if (dataLayer == null)
                throw new NullReferenceException("No data layer instance available.");
            return dataLayer;
        }

        public abstract void Save();

        public abstract void Load(int count);

        public abstract List<IBall> GetBalls();

        public abstract void ClearBalls();

        public abstract void AddBall(IVector initialPosition, IVector initialVelocity, double mass, double circumference);

        public event EventHandler<IBall>? BallAddedNotification;
        
        protected void RaiseBallAddedNotification(IBall ball)
        {
            BallAddedNotification?.Invoke(this, ball);
        }

    }

    public interface IDataLayerFactory
    {
        DataAbstractAPI Get();
    }

    public interface IVector
    {
        double X { get; init; }
        double Y { get; init; }
    }

    public static class VectorFactory
    {
        public static IVector Get(double x, double y)
        {
            return new Vector(x, y);
        }
    }

    public interface IBall
    {
        IVector Position { get; set; }
        IVector Velocity { get; }
        double Mass { get; init; }
        double Circumference { get; init; }
    }
}