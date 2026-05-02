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
        public double MoveDelay
        { 
            get;
            set
            {
                if (!isStarted())
                {
                    field = value;
                }
            }
        }

        private Timer? moveTimer;

        public Ball(IVector initialPosition, IVector initialVelocity, double mass, double circumference)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
            Mass = mass;
            Circumference = circumference;
        }

        private bool isStarted()
        {
            return moveTimer != null;
        }

        internal void Start()
        {
            if (isStarted())
                return;
            
            moveTimer = new Timer(Simulate, null, TimeSpan.Zero, TimeSpan.FromSeconds(MoveDelay));
        }

        internal void Stop()
        {
            if (moveTimer != null)
            {
                moveTimer.Dispose();
                moveTimer = null;
            }
        }

        public event EventHandler<IVector>? NewPositionNotification;

        private void EmitNewPositionNotification()
        {
            NewPositionNotification?.Invoke(this, Position);
        }

        internal void Simulate(object? state)
        {
            Position = new Vector(Position.X + Velocity.X * MoveDelay, Position.Y + Velocity.Y * MoveDelay);
            EmitNewPositionNotification();
        }
    }
}