using Prism.Commands;

namespace TimeManager.ManageTodos.Services
{
    public static class RibbonCommands
    {
        public static DelegateCommand AddTodoCommand { get; set; }
        public static DelegateCommand RemoveTodoCommand { get; set; }

        public static DelegateCommand AddWorkingItemCommand { get; set; }
        public static DelegateCommand RemoveWorkingItemCommand { get; set; }

        public static DelegateCommand ChangeCommand { get; set; }

    }
}
