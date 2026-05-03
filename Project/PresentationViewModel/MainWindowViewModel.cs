using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Project.Presentation.Model;

namespace Project.Presentation.ViewModel;

public class ClickCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly Predicate<object?> canExecute;
    private readonly Action<object?> execute;
    public ClickCommand(Predicate<object?> canExecute, Action<object?> execute, Action<EventHandler>? canExecuteChangedSubscribe = null)
    {
        this.canExecute = canExecute;
        this.execute = execute;

        canExecuteChangedSubscribe?.Invoke((sender, args) => RaiseCanExecuteChanged());
    }

    public ClickCommand(Predicate<object?> canExecute, Action<object?> execute, Action<EventHandler<bool>> canExecuteChangedSubscribe)
    {
        this.canExecute = canExecute;
        this.execute = execute;

        canExecuteChangedSubscribe.Invoke((sender, args) => RaiseCanExecuteChanged());
    }

    public bool CanExecute(object? parameter)
    {
        return canExecute(parameter);
    }
    
    public void Execute(object? parameter)
    {
        execute(parameter);
    }

    private void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}

public class MainWindowViewModel : ViewModelBase
{
    private readonly ModelAbstractAPI modelLayer;

    public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();
    public ICommand CreateBallCommand { get; init; }
    public ICommand ClearBallsCommand { get; init; }
    public ICommand StartCommand 
    {
        get;
        private set
        {
            field = value;
            RaisePropertyChanged();
        }
    }

    public ICommand StopCommand { get; init; }
    public ICommand QuitCommand { get; init; }
    public decimal InitialBallCount { get; set; }
    public int NumberOfBalls
    {
        get;
        private set
        {
            field = value;
            RaisePropertyChanged();
        }
    }

    public bool IsInitializing => !modelLayer.IsInitialized();
    public double AreaX => modelLayer.GetAreaX();
    public double AreaY => modelLayer.GetAreaY();

    public Avalonia.Thickness AreaBorder
    {
        get
        {
            return new Avalonia.Thickness(modelLayer.GetAreaBorder());
        }
    }

    public MainWindowViewModel(ModelAbstractAPI? modelLayer = null)
    {
        if (modelLayer == null)
            modelLayer = ModelAbstractAPI.GetModelLayer();
        this.modelLayer = modelLayer;

        modelLayer.BallAddedNotification += (sender, ball) => {
            Balls.Add(ball);
            NumberOfBalls += 1;
        };
        modelLayer.BallsClearedNotification += (sender, e) => {
            Balls.Clear();
            NumberOfBalls = 0;
        };
        modelLayer.InitializedNotification += (sender, e) => RaisePropertyChanged(nameof(IsInitializing));

        CreateBallCommand = new ClickCommand(
            p => modelLayer.IsInitialized(),
            p => modelLayer.CreateBall(),
            handler => modelLayer.InitializedNotification += handler
        );
        ClearBallsCommand = new ClickCommand(
            p => modelLayer.IsInitialized(),
            p => modelLayer.ClearBalls(),
            handler => modelLayer.InitializedNotification += handler
        );
        StartCommand = new ClickCommand(p => true, p => Initialize());
        StopCommand = new ClickCommand(
            p => modelLayer.IsStarted(),
            p => modelLayer.Stop(),
            handler => modelLayer.IsStartedChangedNotification += handler
        );
        QuitCommand = new ClickCommand(p => true, p => modelLayer.Quit());
        InitialBallCount = 10;
    }

    private void Initialize()
    {
        modelLayer.Initialize((int)InitialBallCount);
        modelLayer.Start();
        StartCommand = new ClickCommand(
            p => !modelLayer.IsStarted(),
            p => modelLayer.Start(),
            handler => modelLayer.IsStartedChangedNotification += handler
        );
    }
}

