using System;
using System.Threading;

namespace Project.Logic
{
    internal class Ball : IBall
    {
        public IVector Position { get; private set; }
        public IVector Velocity { get; private set; }
        public double Mass { get; init; }
        public double Circumference { get; init; }
        private double MoveDelay { get; init; }

        private readonly Timer moveTimer;

        public Ball(IVector initialPosition, IVector initialVelocity, double mass, double circumference, double moveDelay)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
            Mass = mass;
            Circumference = circumference;
            MoveDelay = moveDelay;
            moveTimer = new Timer(Simulate, null, TimeSpan.Zero, TimeSpan.FromSeconds(moveDelay));
        }

        public event EventHandler<IVector>? NewPositionNotification;

        private void EmitNewPositionNotification()
        {
            NewPositionNotification?.Invoke(this, Position);
        }

        private void Simulate(object? state)
        {
            Position = new Vector(Position.X + Velocity.X * MoveDelay, Position.Y + Velocity.Y * MoveDelay);
            EmitNewPositionNotification();
        }
    }
}