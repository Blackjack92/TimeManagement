using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.ViewModels
{
    public interface ISelectedTodoProvider
    {
        Todo SelectedTodo { get; }
    }
}
