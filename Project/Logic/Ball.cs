using System;
using System.Threading;

namespace Project.Logic
{
    internal class Ball : IBall, ICollisionObject
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

        private LogicAbstractAPI logic;

        private Timer? moveTimer;

        public Ball(IVector initialPosition, IVector initialVelocity, double mass, double circumference, LogicAbstractAPI logic)
        {
            Position = initialPosition;
            Velocity = initialVelocity;
            Mass = mass;
            Circumference = circumference;
            this.logic = logic;
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
            Vector movement = new Vector(Velocity.X * MoveDelay, Velocity.Y * MoveDelay);
            CollisionInfo collisionInfo = logic.GetArea().Collide(this, movement);
            if (collisionInfo.Collided)
            {
                movement *= collisionInfo.MoveFraction;
                Velocity = collisionInfo.NewVelocity;
                movement += collisionInfo.NewVelocity * (1 - collisionInfo.MoveFraction) * MoveDelay;
            }

            Position = new Vector(Position.X + movement.X, Position.Y + movement.Y);
            EmitNewPositionNotification();
        }

        public CollisionInfo Collide(ICollisionObject collidingObject, IVector movement)
        {
            return new CollisionInfo();
        }
    }
}