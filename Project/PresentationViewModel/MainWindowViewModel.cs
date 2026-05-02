using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using Project.Presentation.Model;

namespace Project.Presentation.ViewModel;

public class ClickCommand : ICommand
{
    private readonly ModelAbstractAPI modelLayer;
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

    public MainWindowViewModel(ModelAbstractAPI modelLayer = null)
    {
        if (modelLayer == null)
            modelLayer = ModelAbstractAPI.GetModelLayer();
        this.modelLayer = modelLayer;

        modelLayer.BallAddedNotification += (sender, ball) => Balls.Add(ball);
        modelLayer.BallsClearedNotification += (sender, e) => Balls.Clear();
    }

    public bool predicateFun() //test
    {
        return true;
    }

    public ICommand CreateBallCommand()
    {
        return new ClickCommand(p => predicateFun(), p => modelLayer.CreateBall());
    }
    public ICommand ClearBallsCommand()
    {
        return new ClickCommand(p => predicateFun(), p => modelLayer.ClearBalls());
    }
    public ICommand StartCommand()
    {
        return new ClickCommand(p => predicateFun(), p => modelLayer.Start());
    }
    public ICommand StopCommand()
    {
        return new ClickCommand(p => predicateFun(), p => modelLayer.Stop());
    }
    public ICommand QuitCommand()
    {
        return new ClickCommand(p => predicateFun(), p => modelLayer.Quit());
    }

    public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();

}

