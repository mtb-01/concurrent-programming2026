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
    public double Diameter { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Ball(double positionX, double positionY, double mass, double diameter, ILogicBall logicBall)
    {
        PositionX = positionX - diameter/2;
        PositionY = positionY - diameter/2;
        Mass = mass;
        Diameter = diameter;
        logicBall.NewPositionNotification += OnNewPositionNotification;
    }

    private void OnNewPositionNotification(object? sender, IVector newPosition)
    {
        PositionX = newPosition.X - Diameter/2;
        PositionY = newPosition.Y - Diameter/2;
    }

    private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
