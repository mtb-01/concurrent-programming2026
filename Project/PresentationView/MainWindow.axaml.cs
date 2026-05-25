using Avalonia.Controls;
using Project.Presentation.ViewModel;
using Project.Data;
using Project.Logic;
using Project.Presentation.Model;

namespace Project.Presentation.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        IDataLayerFactory dataLayerFactory = new DataImplementationFactory()
        {
            XPositionRange = VectorFactory.Get(16, 584),
            YPositionRange = VectorFactory.Get(16, 384),
            XVelocityRange = VectorFactory.Get(-200, 200),
            YVelocityRange = VectorFactory.Get(-200, 200),
            MassRange = VectorFactory.Get(10, 40),
            DiameterRange = VectorFactory.Get(22, 32)
        };
        DataAbstractAPI.SetDataLayer(dataLayerFactory.Get());

        ILogicLayerFactory logicLayerFactory = new LogicImplementationFactory()
        {
            AreaX = 600,
            AreaY = 400
        };
        LogicAbstractAPI.SetLogicLayer(logicLayerFactory.Get());
        
        IModelLayerFactory modelLayerFactory = new ModelImplementationFactory()
        {
            MassRangeMin = 10,
            MassRangeMax = 40
        };
        ModelAbstractAPI.SetModelLayer(modelLayerFactory.Get());

        MainWindowViewModel viewModel = new MainWindowViewModel();
        DataContext = viewModel;
        InitializeComponent();
    }
}
