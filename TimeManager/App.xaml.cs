using System.Windows;

namespace TimeManager.Shell
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper bs = new Bootstrapper();
            bs.Run();
        }
    }
}
