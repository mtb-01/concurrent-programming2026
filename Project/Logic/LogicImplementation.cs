using System.Collections.Generic;
using Project.Data;

namespace Project.Logic
{
    internal class LogicImplementation : LogicAbstractAPI
    {
        private readonly List<Ball> listOfBalls = new List<Ball>();
        private readonly Area area;
        private bool isLayerStarted = false;
        private bool isStarted
        {
            get;
            set
            {
                field = value;
                RaiseIsStartedChangedNotification(value);
            }
        }
        private double moveDelay;
        private DataAbstractAPI data;

        public LogicImplementation (double areaX, double areaY, DataAbstractAPI? data = null)
        {
            area = new Area(areaX, areaY);

            if (data == null)
                data = DataAbstractAPI.GetDataLayer();
            this.data = data;

            data.BallAddedNotification += (sender, dataBall) =>
            {
                IVector position = new Vector(dataBall.Position.X, dataBall.Position.Y);
                IVector velocity = new Vector(dataBall.Velocity.X, dataBall.Velocity.Y);
                Ball ball = new Ball(position, velocity, dataBall.Mass, dataBall.Diameter, this);
                AddBall(ball);
            };
        }

        public override void StartLayer(int initialBallCount)
        {
            if (isLayerStarted)
                return;

            isLayerStarted = true;
            data.Load(initialBallCount);
        }

        public override void Start(double moveDelay)
        {
            if (isStarted || !isLayerStarted)
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
            if (!isStarted || !isLayerStarted)
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
            if (!isLayerStarted)
                return;
            
            data.Load(1);
        }

        public override void ClearBalls()
        {
            if (!isLayerStarted)
                return;

            data.ClearBalls();
            listOfBalls.Clear();
            RaiseBallsClearedNotification();
        }

        private void AddBall(Ball ball)
        {
            if (!isLayerStarted)
                return;

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

        public override IVector GetAreaSize()
        {
            return new Vector(area.SizeX, area.SizeY);
        }

        internal override ICollisionObject GetArea()
        {
            return area;
        }
    }
}