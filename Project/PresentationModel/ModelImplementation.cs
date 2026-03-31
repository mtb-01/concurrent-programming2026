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
        logicLayer.BallAddedNotification += (sender, ball) => RaiseBallAddedNotification(new Ball(ball.Position.X, ball.Position.Y, ball));
        logicLayer.BallsClearedNotification += (sender, e) => RaiseBallsClearedNotification();
    }

    public override void Start()
    {
        logicLayer.Start(1.0 / 60.0);
    }

}
