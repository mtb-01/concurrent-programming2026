using System.Collections.Generic;
using Project.Data;
using IDataBall = Project.Data.IBall;

namespace Project.Logic
{
    internal class LogicImplementation : LogicAbstractAPI
    {
        private readonly List<IBall> listOfBalls = new List<IBall>();
        private DataAbstractAPI data;

        public LogicImplementation (DataAbstractAPI? data = null)
        {
            if (data == null)
                data = DataAbstractAPI.GetDataLayer();
            this.data = data;
        }

        public override void Start(double moveDelay)
        {
            data.Load();
            foreach(IDataBall dataBall in data.GetBalls())
            {
                IVector position = new Vector(dataBall.Position.X, dataBall.Position.Y);
                IVector velocity = new Vector(dataBall.Velocity.X, dataBall.Velocity.Y);
                Ball ball = new Ball(position, velocity, dataBall.Mass, dataBall.Circumference, moveDelay);
                ball.Start();
                AddBall(ball);
            }
        }

        private void AddBall(IBall ball)
        {
            listOfBalls.Add(ball);
            RaiseBallAddedNotification(ball);
        }
    }
}