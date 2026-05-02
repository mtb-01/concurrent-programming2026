using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Project.Presentation.Model;

namespace Project.Presentation.ViewModel;

public class ClickCommand : ICommand
{
    public event EventHandler CanExecuteChanged;
    private readonly Predicate<object> canExecute;
    private readonly Action<object> execute;
    public ClickCommand(Predicate<object> canExecute, Action<object> execute)
    {
        this.canExecute = canExecute;
        this.execute = execute;
    }

    public bool CanExecute(object parameter)
    {
        return canExecute(parameter);
    }
    
    public void Execute(object parameter)
    {
        execute(parameter);
    }

}

public class MainWindowViewModel : ViewModelBase
{
    protected readonly ModelAbstractAPI modelLayer;
    public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();
    public ICommand CreateBallCommand { get; private set; }
    public ICommand ClearBallsCommand { get; private set; }
    public ICommand StartCommand { get; private set; }
    public ICommand StopCommand { get; private set; }
    public ICommand QuitCommand { get; private set; }
    public int NumberOfBalls;

    public MainWindowViewModel(ModelAbstractAPI modelLayer = null)
    {
        if (modelLayer == null)
            modelLayer = ModelAbstractAPI.GetModelLayer();
        this.modelLayer = modelLayer;

        modelLayer.BallAddedNotification += (sender, ball) => Balls.Add(ball);
        modelLayer.BallsClearedNotification += (sender, e) => Balls.Clear();
        CreateBallCommand = new ClickCommand(p => predicateFun(), p => modelLayer.CreateBall());
        ClearBallsCommand = new ClickCommand(p => predicateFun(), p => modelLayer.ClearBalls());
        StartCommand = new ClickCommand(p => predicateFun(), p => modelLayer.Start());
        StopCommand = new ClickCommand(p => predicateFun(), p => modelLayer.Stop());
        QuitCommand = new ClickCommand(p => predicateFun(), p => modelLayer.Quit());
        NumberOfBalls = Balls.Count;
    }

    public bool predicateFun()
    {
        return true;
    }

    public void StartLayer()
    {
        modelLayer.StartLayer();
    }

}

