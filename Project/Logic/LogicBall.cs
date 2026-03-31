using System;
using System.Collections.Generic;
using System.Threading;

namespace Project.Logic
{
    internal class Ball : IBall
    {
        public IVector Position { get; set; }
        public IVector Velocity { get; }
        public double Mass { get; init; }
        public double Circumference { get; init; }
        private double MoveDelay { get; init; }
        private readonly Timer MoveTimer { get; }

        internal Ball(IVector initialPosition, IVector initialVelocity, double mass, double circumference, double moveDelay)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
            MoveDelay = moveDelay;
        }
        public event EventHandler<IVector>? NewPositionNotification;

        public void EmitNewPositionNotification()
        {
            NewPositionNotification?.Invoke(this, Position);
        }

        public void Move()
        {
            Position = new Vector(Position.x + Velocity.x * MoveDelay, Position.y + Velocity.y * MoveDelay);
            EmitNewPositionNotification();
        }
    }
}