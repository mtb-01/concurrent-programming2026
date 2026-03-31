using System.Collections.ObjectModel;

namespace Project.Presentation.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<string> Balls { get; } = new ObservableCollection<string>() {"Test", "Test1", "Test2"};

    }
}
