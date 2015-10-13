using Prism.Commands;

namespace TimeManager.Shell.Services
{
    public static class ApplicationMenuCommands
    {
        public static DelegateCommand OpenCommand { get; set; }
        public static DelegateCommand SaveCommand { get; set; }
    }
}
