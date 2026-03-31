using Avalonia.Controls;
using Project.Presentation.ViewModel;

namespace Project.Presentation.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        DataContext = new MainWindowViewModel();
        InitializeComponent();
    }
}
