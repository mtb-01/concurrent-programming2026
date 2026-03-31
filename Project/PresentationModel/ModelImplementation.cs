

using System;

namespace Project.Presentation.Model;

internal class LogicAbstractAPI
{
    public event EventHandler<IBall>? BallAddedNotification;
    public event EventHandler? BallsClearedNotification;
}

internal class ModelImplementation : ModelAbstractAPI
{
    private readonly LogicAbstractAPI logicLayer;

    public ModelImplementation(LogicAbstractAPI logicLayer)
    {
        this.logicLayer = logicLayer;
        logicLayer.BallAddedNotification += (sender, ball) => RaiseBallAddedNotification(ball);
        logicLayer.BallsClearedNotification += (sender, e) => RaiseBallsClearedNotification();
    }

    
}