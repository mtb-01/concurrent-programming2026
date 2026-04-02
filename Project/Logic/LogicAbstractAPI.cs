using System;
using System.Collections.Generic;

namespace Project.Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI GetLogicLayer()
        {
            return new LogicImplementation();
        }

        public abstract void Start(double moveDelay);

        public event EventHandler<IBall>? BallAddedNotification;
        public event EventHandler? BallsClearedNotification;

        protected void RaiseBallAddedNotification(IBall ball)
        {
            BallAddedNotification?.Invoke(this, ball);
        }

        protected void RaiseBallsClearedNotification()
        {
            BallsClearedNotification?.Invoke(this, EventArgs.Empty);
        }

        public abstract List<IBall> GetBalls();
    }

    public interface IBall
    {
        IVector Position { get; }
        IVector Velocity { get; }
        double Mass { get; init; }
        double Circumference { get; init; }

        event EventHandler<IVector>? NewPositionNotification;
    }

    public interface IVector
    {
        double X { get; init; }
        double Y { get; init; }
    }
}