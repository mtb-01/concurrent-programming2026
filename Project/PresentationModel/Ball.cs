using System.ComponentModel;
using System.Runtime.CompilerServices;
using Project.Logic;
using ILogicBall = Project.Logic.IBall;

namespace Project.Presentation.Model;


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

    public double Mass { get; init; }
    public double Circumference { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Ball(double positionX, double positionY, double mass, double circumference, ILogicBall logicBall)
    {
        PositionX = positionX;
        PositionY = positionY;
        Mass = mass;
        Circumference = circumference;
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
