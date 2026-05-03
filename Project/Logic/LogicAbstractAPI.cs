using System;
using System.Collections.Generic;

namespace Project.Logic
{
    public abstract class LogicAbstractAPI
    {
        private static LogicAbstractAPI? logicLayer;

        public static void SetLogicLayer(LogicAbstractAPI logicLayer)
        {
            LogicAbstractAPI.logicLayer = logicLayer;
        }

        public static LogicAbstractAPI GetLogicLayer()
        {
            if (logicLayer == null)
                throw new NullReferenceException("No logic layer instance available.");
            return logicLayer;
        }

        public abstract void StartLayer(int initialBallCount);

        public abstract void Start(double moveDelay);

        public abstract void Stop();

        public abstract bool IsStarted();

        public abstract void CreateBall();

        public abstract void ClearBalls();

        public abstract List<IBall> GetBalls();

        public abstract IVector GetAreaSize();

        internal abstract ICollisionObject GetArea();

        public event EventHandler<bool>? IsStartedChangedNotification;
        public event EventHandler<IBall>? BallAddedNotification;
        public event EventHandler? BallsClearedNotification;

        protected void RaiseIsStartedChangedNotification(bool isStarted)
        {
            IsStartedChangedNotification?.Invoke(this, isStarted);
        }

        protected void RaiseBallAddedNotification(IBall ball)
        {
            BallAddedNotification?.Invoke(this, ball);
        }

        protected void RaiseBallsClearedNotification()
        {
            BallsClearedNotification?.Invoke(this, EventArgs.Empty);
        }
    }

    public interface ILogicLayerFactory
    {
        LogicAbstractAPI Get();
    }

    public interface IBall
    {
        IVector Position { get; }
        IVector Velocity { get; }
        double Mass { get; init; }
        double Diameter { get; init; }

        event EventHandler<IVector>? NewPositionNotification;
    }

    public interface IVector
    {
        double X { get; init; }
        double Y { get; init; }
    }
}