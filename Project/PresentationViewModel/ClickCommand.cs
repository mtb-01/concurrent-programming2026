using System.Windows.Input;
using System;

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