using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project.Presentation.Model;

internal interface IVector
{
    double X { get; }
    double Y { get; }
}

internal interface ILogicBall
{
    event EventHandler<IVector> NewPositionNotification;
}

internal class Ball : IBall
{
    public double PositionX 
    {
        get;
        set
        {
            if (field == value)
                return;
            field = value;
            RaisePropertyChanged();
        }
    }
    
    public double PositionY 
    {
        get;
        set
        {
            if (field == value)
                return;
            field = value;
            RaisePropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Ball(double positionX, double positionY, ILogicBall logicBall)
    {
        PositionX = positionX;
        PositionY = positionY;
        logicBall.NewPositionNotification += OnNewPositionNotification;
    }

    private void OnNewPositionNotification(object? sender, IVector newPosition)
    {
        PositionX = newPosition.X;
        PositionY = newPosition.Y;
    }

    private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
