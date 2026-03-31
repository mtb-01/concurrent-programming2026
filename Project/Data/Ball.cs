using System;

namespace Project.Data
{
    internal class Ball : IBall
    {
        public IVector Position { get; set; }
        public IVector Velocity { get; }
        public double Mass { get; init; }
        public double Circumference { get; init; }


        internal Ball(IVector initialPosition, IVector initialVelocity, double mass, double circumference)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
        }
        public event EventHandler<IVector>? NewPositionNotification;
        
        internal void Move(Vector delta)
        {
            Position = new Vector(Position.x + delta.x, Position.y + delta.y);
            EmitNewPositionNotification();
        }

        private void EmitNewPositionNotification()
        {
            NewPositionNotification?.Invoke(this, Position);
        }

    }
}
