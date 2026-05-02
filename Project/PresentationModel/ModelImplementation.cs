using System;
using Project.Logic;

namespace Project.Presentation.Model;

internal class ModelImplementation : ModelAbstractAPI
{
    private readonly LogicAbstractAPI logicLayer;

    public ModelImplementation(LogicAbstractAPI? logicLayer = null)
    {
        if (logicLayer == null)
            logicLayer = LogicAbstractAPI.GetLogicLayer();
        this.logicLayer = logicLayer;
        
        logicLayer.BallAddedNotification += (sender, ball) => RaiseBallAddedNotification(
                new Ball(ball.Position.X, ball.Position.Y, ball.Mass, ball.Circumference, ball)
            );
        logicLayer.BallsClearedNotification += (sender, e) => RaiseBallsClearedNotification();
    }

    public override void ClearBalls()
    {
        logicLayer.ClearBalls();
    }

    public override void CreateBall()
    {
        logicLayer.CreateBall();
    }

    public override void Quit()
    {
        Environment.Exit(0);
    }

    public override void Start()
    {
        logicLayer.Start(1.0 / 60.0);
    }

    public override void Stop()
    {
        logicLayer.Stop();
    }
}
