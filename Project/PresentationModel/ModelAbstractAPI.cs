using System;
using System.ComponentModel;

namespace Project.Presentation.Model;

public abstract class ModelAbstractAPI
{
    private static ModelAbstractAPI? modelLayer;

    public static void SetModelLayer(ModelAbstractAPI modelLayer)
    {
        ModelAbstractAPI.modelLayer = modelLayer;
    }

    public static ModelAbstractAPI GetModelLayer()
    {
        if (modelLayer == null)
            throw new NullReferenceException("No model layer instance available.");
        return modelLayer;
    }

    public abstract void Initialize(int initialBallCount);
    public abstract bool IsInitialized();
    public abstract void CreateBall();
    public abstract void ClearBalls();
    public abstract void Start();
    public abstract void Stop();
    public abstract bool IsStarted();
    public abstract void Quit();
    public abstract double GetAreaX();
    public abstract double GetAreaY();
    public abstract double GetAreaBorder();
    public abstract int GetElapsedSeconds();
    public abstract int GetElapsedMinutes();

    public event EventHandler? InitializedNotification;
    public event EventHandler<bool>? IsStartedChangedNotification;
    public event EventHandler<IBall>? BallAddedNotification;
    public event EventHandler? BallsClearedNotification;
    public event EventHandler? ElapsedTimeChangedNotification;

    protected void RaiseInitializedNotification()
    {
        InitializedNotification?.Invoke(this, EventArgs.Empty);
    }

    protected void RaiseIsStartedChangedNotification(bool isStarted)
    {
        IsStartedChangedNotification?.Invoke(this, isStarted);
    }

    protected void RaiseBallAddedNotification(IBall ball)
    {
        BallAddedNotification?.Invoke(this, ball);
    }

    protected void RaiseBallsClearedNotification()
    {
        BallsClearedNotification?.Invoke(this, EventArgs.Empty);
    }

    protected void RaiseElapsedTimeChangedNotification()
    {
        ElapsedTimeChangedNotification?.Invoke(this, EventArgs.Empty);
    }
}

public interface IBall : INotifyPropertyChanged
{
    double PositionX { get; }
    double PositionY { get; }
    double Color { get; }
    double Diameter { get; init; }
}

public interface IModelLayerFactory
{
    ModelAbstractAPI Get();
}
