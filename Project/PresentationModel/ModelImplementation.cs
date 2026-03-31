

using System;

namespace Project.Presentation.Model;

internal class LogicAbstractAPI
{
    public static LogicAbstractAPI GetLogicLayer()
    {
        return new LogicAbstractAPI();
    }

    public event EventHandler<IBall>? BallAddedNotification;
    public event EventHandler? BallsClearedNotification;
}

internal class ModelImplementation : ModelAbstractAPI
{
    private readonly LogicAbstractAPI logicLayer;

    public ModelImplementation(LogicAbstractAPI? logicLayer = null)
    {
        if (logicLayer == null)
            logicLayer = LogicAbstractAPI.GetLogicLayer();
        this.logicLayer = logicLayer;
        logicLayer.BallAddedNotification += (sender, ball) => RaiseBallAddedNotification(ball);
        logicLayer.BallsClearedNotification += (sender, e) => RaiseBallsClearedNotification();
    }

    
}
