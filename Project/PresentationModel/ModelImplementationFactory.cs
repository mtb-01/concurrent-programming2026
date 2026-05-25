namespace Project.Presentation.Model;

public class ModelImplementationFactory : IModelLayerFactory
{
    required public double MassRangeMin { get; set; }
    required public double MassRangeMax { get; set; }

    public ModelAbstractAPI Get()
    {
        return new ModelImplementation()
        {
            MassRangeMax = this.MassRangeMax,
            MassRangeMin = this.MassRangeMin
        };
    }
}
