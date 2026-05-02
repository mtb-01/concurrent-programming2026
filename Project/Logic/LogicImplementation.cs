using System.Collections.Generic;
using Project.Data;

namespace Project.Logic
{
    internal class LogicImplementation : LogicAbstractAPI
    {
        public int InitialBallCount { get; init; }

        private readonly List<Ball> listOfBalls = new List<Ball>();
        private readonly Area area;
        private bool isStarted = false;
        private double moveDelay;
        private DataAbstractAPI data;

        public LogicImplementation (int initialBallCount, double areaX, double areaY, DataAbstractAPI? data = null)
        {
            InitialBallCount = initialBallCount;
            area = new Area(areaX, areaY);

            if (data == null)
                data = DataAbstractAPI.GetDataLayer();
            this.data = data;

            data.BallAddedNotification += (sender, dataBall) =>
            {
                IVector position = new Vector(dataBall.Position.X, dataBall.Position.Y);
                IVector velocity = new Vector(dataBall.Velocity.X, dataBall.Velocity.Y);
                Ball ball = new Ball(position, velocity, dataBall.Mass, dataBall.Circumference, this);
                AddBall(ball);
            };

            // ?
            data.BallsClearedNotification += (sender, args) =>
            {
                
            };
        }

        public override void Start(double moveDelay)
        {
            if (isStarted)
                return;

            this.moveDelay = moveDelay;
            foreach(Ball ball in listOfBalls)
            {
                ball.Stop();
                ball.MoveDelay = moveDelay;
                ball.Start();
            }
            isStarted = true;
        }

        public override void Stop()
        {
            if (!isStarted)
                return;

            foreach(Ball ball in listOfBalls)
            {
                ball.Stop();
            }
            isStarted = false;
        }

        public override bool IsStarted()
        {
            return isStarted;
        }

        public override void CreateBall()
        {
            data.Load(1);
        }

        public override void ClearBalls()
        {
            data.ClearBalls();
            // ?
            listOfBalls.Clear();
            RaiseBallsClearedNotification();
        }

        private void AddBall(Ball ball)
        {
            listOfBalls.Add(ball);
            RaiseBallAddedNotification(ball);
            if (isStarted)
            {
                ball.MoveDelay = moveDelay;
                ball.Start();
            }
        }

        public override List<IBall> GetBalls()
        {
            return new List<IBall>(listOfBalls);
        }

        internal override ICollisionObject GetArea()
        {
            return area;
        }

        public override void StartLayer()
        {
            data.Load(InitialBallCount);
        }
    }
}