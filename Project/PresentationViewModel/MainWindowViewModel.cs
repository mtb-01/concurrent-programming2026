using System.Collections.ObjectModel;

namespace Project.PresentationViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<string> Balls { get; } = new ObservableCollection<string>() {"Test", "Test1", "Test2"};

    }
}
