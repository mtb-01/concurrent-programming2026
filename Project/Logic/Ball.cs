using System;
using System.Threading;

namespace Project.Logic
{
    internal class Ball : IBall, ICollisionObject
    {
        public int ID { get; private init; }
        public IVector Position { get; private set; }
        public IVector Velocity { get; private set; }
        public double Mass { get; init; }
        public double Diameter { get; init; }
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

        public Lock MovementLock { get; private init; } = new Lock();

        private LogicAbstractAPI logic;

        private Thread? mainThread;
        private CancellationTokenSource? cancelSource;

        private long lastMoveTime;


        public Ball(int id, IVector initialPosition, IVector initialVelocity, double mass, double diameter, LogicAbstractAPI logic)
        {
            ID = id;
            Position = initialPosition;
            Velocity = initialVelocity;
            Mass = mass;
            Diameter = diameter;
            this.logic = logic;
        }

        private bool isStarted()
        {
            return mainThread != null && mainThread.IsAlive;
        }

        internal void Start()
        {
            if (isStarted())
                return;
            
            lastMoveTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            cancelSource = new CancellationTokenSource();
            mainThread = new Thread(new ThreadStart(
                () => MainLoop(cancelSource.Token)));
            mainThread.Start();
        }

        internal void Stop()
        {
            cancelSource?.Cancel();
        }

        public event EventHandler<IVector>? NewPositionNotification;

        private void EmitNewPositionNotification()
        {
            NewPositionNotification?.Invoke(this, Position);
        }

        private void MainLoop(CancellationToken cancelToken)
        {
            while(true)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(MoveDelay));
                Simulate();
                logic.WriteDiagnostic("Kulka " + ID + ": Pozycja (" +
                    Position.X + ", " + Position.Y + ") Prędkość (" +
                    Velocity.X + ", " + Velocity.Y + ")");
            }
        }

        internal void Simulate()
        {
            long moveTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            double moveTimeDelta = (moveTime - lastMoveTime) / 1000.0;
            lastMoveTime = moveTime;

            Vector CollideAndReturnRemainder(ICollisionObject collisionObject, Vector movement, string collisionDiagnosticText)
            {
                CollisionInfo collisionInfo = collisionObject.Collide(this, movement);
                if (collisionInfo.Collided)
                {
                    movement *= collisionInfo.MoveFraction;
                    Velocity = collisionInfo.NewVelocity;
                    Position = new Vector(Position.X + movement.X, Position.Y + movement.Y);
                    movement = collisionInfo.NewVelocity * (1 - collisionInfo.MoveFraction) * moveTimeDelta;

                    logic.WriteDiagnostic(collisionDiagnosticText);
                }
                return movement;
            }

            Vector movement = new Vector();
            MovementLock.Enter();
            try
            {
                movement = new Vector(Velocity.X * moveTimeDelta, Velocity.Y * moveTimeDelta);
            }
            finally { MovementLock.Exit(); }

            foreach (Ball ball in logic.GetLogicBalls())
            {
                if (ball.ID < ID)
                {
                    MovementLock.Enter();
                    try
                    {
                        ball.MovementLock.Enter();
                        try
                        {
                            movement = CollideAndReturnRemainder(ball, movement, "Kulka " + ID + ": Kolizja z kulką " + ball.ID);
                        }
                        finally { ball.MovementLock.Exit(); }
                    }
                    finally { MovementLock.Exit(); }
                }
                else if (ball.ID > ID)
                {
                    ball.MovementLock.Enter();
                    try
                    {
                        MovementLock.Enter();
                        try
                        {
                            movement = CollideAndReturnRemainder(ball, movement, "Kulka " + ID + ": Kolizja z kulką " + ball.ID);
                        }
                        finally { MovementLock.Exit(); }
                    }
                    finally { ball.MovementLock.Exit(); }
                }
                if (movement.Equals(new Vector()))
                {
                    EmitNewPositionNotification();
                    return;
                }
            }

            MovementLock.Enter();
            try
            {
                movement = CollideAndReturnRemainder(logic.GetArea(), movement, "Kulka " + ID + ": Kolizja ze ścianą");
                Position = new Vector(Position.X + movement.X, Position.Y + movement.Y);
            }
            finally { MovementLock.Exit(); }

            EmitNewPositionNotification();
        }

        public CollisionInfo Collide(ICollisionObject collidingObject, IVector movement)
        {
            IBall? collidingBall = collidingObject as IBall;
            if (collidingBall != null)
            {
                return CollideBall(collidingBall, movement);
            }
            return new CollisionInfo();
        }

        private CollisionInfo CollideBall(IBall collidingBall, IVector movement)
        {
            Vector ballMovePosition = new Vector(collidingBall.Position.X + movement.X, collidingBall.Position.Y + movement.Y);

            double distanceSquared = Math.Pow(this.Position.X - ballMovePosition.X, 2) + Math.Pow(this.Position.Y - ballMovePosition.Y, 2);
            double radiusSum = this.Diameter / 2 + collidingBall.Diameter / 2;

            if (Math.Pow(radiusSum, 2) < distanceSquared)
            {
                return new CollisionInfo();
            }

            double moveFraction = radiusSum - new Vector(this.Position.X - collidingBall.Position.X, this.Position.Y - collidingBall.Position.Y).Length();
            moveFraction = Math.Clamp(moveFraction / new Vector(movement.X, movement.Y).Length(), 0, 1);

            Vector collisionPosition = new Vector(collidingBall.Position.X + movement.X * moveFraction, collidingBall.Position.Y + movement.Y * moveFraction);
            Vector collisionDirection = new Vector(this.Position.X - collisionPosition.X, this.Position.Y - collisionPosition.Y).Normalized();

            Vector ballVelocity = new Vector(collidingBall.Velocity.X, collidingBall.Velocity.Y);
            Vector thisVelocity = new Vector(this.Velocity.X, this.Velocity.Y);

            double ballVelocityProjectedLength = ballVelocity.Dot(collisionDirection);
            if (ballVelocityProjectedLength <= 0)
            {
                return new CollisionInfo();
            }

            double thisVelocityProjectedLength = thisVelocity.Dot(collisionDirection);

            double massSum = this.Mass + collidingBall.Mass;

            double newBallVelocityProjectedLength = (thisVelocityProjectedLength * (2*this.Mass) +
                ballVelocityProjectedLength * (collidingBall.Mass - this.Mass)) / massSum;

            double newThisVelocityProjectedLength = (ballVelocityProjectedLength * (2*collidingBall.Mass) +
                thisVelocityProjectedLength * (this.Mass - collidingBall.Mass)) / massSum;
            
            Vector ballNewVelocity = ballVelocity + collisionDirection * (newBallVelocityProjectedLength - ballVelocityProjectedLength);
            Vector thisNewVelocity = thisVelocity + collisionDirection * (newThisVelocityProjectedLength - thisVelocityProjectedLength);

            this.Velocity = thisNewVelocity;

            return new CollisionInfo(true, moveFraction, ballNewVelocity);
        }
    }
}