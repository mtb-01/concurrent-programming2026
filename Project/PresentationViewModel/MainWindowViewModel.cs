using System.Collections.ObjectModel;
using Project.Presentation.Model;

namespace Project.Presentation.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ModelAbstractAPI modelLayer;

    public MainWindowViewModel(ModelAbstractAPI modelLayer = null)
    {
        if (modelLayer == null)
            modelLayer = ModelAbstractAPI.GetModelLayer();
        this.modelLayer = modelLayer;
        modelLayer.BallAddedNotification += (sender, ball) => Balls.Add(ball);
        modelLayer.BallsClearedNotification += (sender, e) => Balls.Clear();
    }

    public void Start()
    {
        modelLayer.Start();
    }

    public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();

}

