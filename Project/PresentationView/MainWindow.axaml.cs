using Avalonia.Controls;
using Project.Presentation.ViewModel;
using Project.Data;

namespace Project.Presentation.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        IDataLayerFactory dataLayerFactory = new DataImplementationFactory()
        {
            XPositionRange = VectorFactory.Get(0, 400),
            YPositionRange = VectorFactory.Get(0, 400),
            XVelocityRange = VectorFactory.Get(-20, 20),
            YVelocityRange = VectorFactory.Get(-20, 20),
            MassRange = VectorFactory.Get(10, 20),
            CircumferenceRange = VectorFactory.Get(10, 20)
        };
        DataAbstractAPI.SetDataLayer(dataLayerFactory.Get());

        MainWindowViewModel viewModel = new MainWindowViewModel();
        DataContext = viewModel;
        viewModel.StartCommand();
        InitializeComponent();
    }
}
