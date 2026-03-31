using System;
using System.Threading;

namespace Project.Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract void Start();
    }

    public interface IBall
    {
        IVector Position { get; set; }
        IVector Velocity { get; }
        double Mass { get; init; }
        double Circumference { get; init; }
        double MoveDelay { get; init; }
        readonly Timer MoveTimer { get; }

        public event EventHandler<IVector>? NewPositionNotification;
        public void Move();
        public void EmitNewPositionNotification();
    }

    public interface IVector
    {
        double x { get; init; }
        double y { get; init; }
    }
}