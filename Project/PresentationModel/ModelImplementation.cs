using System;
using Project.Logic;

namespace Project.Presentation.Model;

internal class ModelImplementation : ModelAbstractAPI
{
    private readonly LogicAbstractAPI logicLayer;

    private bool isInitialized = false;

    public ModelImplementation(LogicAbstractAPI? logicLayer = null)
    {
        if (logicLayer == null)
            logicLayer = LogicAbstractAPI.GetLogicLayer();
        this.logicLayer = logicLayer;
        
        logicLayer.IsStartedChangedNotification += (sender, isStarted) => RaiseIsStartedChangedNotification(isStarted);

        logicLayer.BallAddedNotification += (sender, ball) => RaiseBallAddedNotification(
                new Ball(ball.Position.X, ball.Position.Y, ball.Mass, ball.Circumference, ball)
            );
        logicLayer.BallsClearedNotification += (sender, e) => RaiseBallsClearedNotification();
    }


    public override void Initialize(int initialBallCount)
    {
        if (isInitialized)
            return;

        logicLayer.StartLayer(initialBallCount);
        isInitialized = true;
        RaiseInitializedNotification();
    }

    public override bool IsInitialized()
    {
        return isInitialized;
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

    public override bool IsStarted()
    {
        return logicLayer.IsStarted();
    }

    public override double GetAreaX()
    {
        return logicLayer.GetAreaSize().X + GetAreaBorder() * 2;
    }

    public override double GetAreaY()
    {
        return logicLayer.GetAreaSize().Y + GetAreaBorder() * 2;
    }

    public override double GetAreaBorder()
    {
        return 4;
    }
}
