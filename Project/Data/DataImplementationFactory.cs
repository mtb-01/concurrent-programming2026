namespace Project.Data;

public class DataImplementationFactory : IDataLayerFactory
{
    required public IVector XPositionRange { get; set; }
    required public IVector YPositionRange { get; set; }
    required public IVector XVelocityRange { get; set; }
    required public IVector YVelocityRange { get; set; }
    required public IVector MassRange { get; set; }
    required public IVector CircumferenceRange { get; set; }

    public DataAbstractAPI Get()
    {
        return new DataImplementation()
        {
            XPositionRange = this.XPositionRange,
            YPositionRange = this.YPositionRange,
            XVelocityRange = this.XVelocityRange,
            YVelocityRange = this.YVelocityRange,
            MassRange = this.MassRange,
            CircumferenceRange = this.CircumferenceRange
        };
    }
}