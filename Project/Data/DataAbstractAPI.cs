using System;

namespace Project.Data
{
    public abstract class DataAbstractAPI
    {
    }

    public interface IVector
    {
        double x {get; init; }
        double y {get; init; }
    }

    public interface IBall
    {
        event EventHandler<IVector> NewPositionNotification;
    }
}