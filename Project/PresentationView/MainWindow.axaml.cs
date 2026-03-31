using Avalonia.Controls;
using Project.Presentation.ViewModel;

namespace Project.Presentation.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();
        DataContext = viewModel;
        viewModel.Start();
        InitializeComponent();
    }
}
