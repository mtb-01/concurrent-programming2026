namespace Project.Logic;

class LogicImplementationFactory : ILogicLayerFactory
{
    required public int InitialBallCount { get; set; }

    public LogicAbstractAPI Get()
    {
        return new LogicImplementation(InitialBallCount);
    }
}