using System;
using System.ComponentModel;

namespace Project.Presentation.Model;

public abstract class ModelAbstractAPI
{
    public static ModelAbstractAPI GetModelLayer()
    {
        return new ModelImplementation();
    }

    public abstract void StartLogicLayer(int initialBallCount);
    public abstract void CreateBall();
    public abstract void ClearBalls();
    public abstract void Start();
    public abstract void Stop();
    public abstract void Quit();
    public abstract double GetAreaX();
    public abstract double GetAreaY();

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
    double Mass { get; init; }
    double Circumference { get; init; }
}
