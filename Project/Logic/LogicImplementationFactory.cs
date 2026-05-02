namespace Project.Logic;

public class LogicImplementationFactory : ILogicLayerFactory
{
    required public double AreaX { get; set; }
    required public double AreaY { get; set; }
    required public int InitialBallCount { get; set; }

    public LogicAbstractAPI Get()
    {
        return new LogicImplementation(InitialBallCount, AreaX, AreaY);
    }
}
