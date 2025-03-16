using System.Windows.Input;

namespace IdentecSolutions.Application.Core.Commands
{
    public abstract record BaseCommand :ICommand
    { }
    public abstract record BaseCommand<T>: ICommand<T>
    {
    }
}
