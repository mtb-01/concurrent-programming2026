using System;
using System.ComponentModel;

namespace Project.Presentation.Model;

public abstract class ModelAbstractAPI
{
    public static ModelAbstractAPI GetModelLayer()
    {
        return new ModelImplementation();
    }

    public abstract void Start();

    public event EventHandler<IBall>? BallAddedNotification;
    public event EventHandler? BallsClearedNotification;

    protected void RaiseBallAddedNotification(IBall ball)
    {
        BallAddedNotification?.Invoke(this, ball);
    }

    protected void RaiseBallsClearedNotification()
    {
        BallsClearedNotification?.Invoke(this, EventArgs.Empty);
    }
}

public interface IBall : INotifyPropertyChanged
{
    double PositionX { get; }
    double PositionY { get; }
}
