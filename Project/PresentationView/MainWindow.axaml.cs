using Avalonia.Controls;
using Project.Presentation.ViewModel;
using Project.Data;
using Project.Logic;

namespace Project.Presentation.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        IDataLayerFactory dataLayerFactory = new DataImplementationFactory()
        {
            XPositionRange = VectorFactory.Get(10, 390),
            YPositionRange = VectorFactory.Get(10, 390),
            XVelocityRange = VectorFactory.Get(-200, 200),
            YVelocityRange = VectorFactory.Get(-200, 200),
            MassRange = VectorFactory.Get(10, 20),
            CircumferenceRange = VectorFactory.Get(10, 20)
        };
        DataAbstractAPI.SetDataLayer(dataLayerFactory.Get());

        ILogicLayerFactory logicLayerFactory = new LogicImplementationFactory()
        {
            AreaX = 400,
            AreaY = 400
        };
        LogicAbstractAPI.SetLogicLayer(logicLayerFactory.Get());
        
        MainWindowViewModel viewModel = new MainWindowViewModel();
        DataContext = viewModel;
        InitializeComponent();
    }
}
