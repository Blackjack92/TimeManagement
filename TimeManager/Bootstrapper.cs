using TimeManager.ManageProjects;
using TimeManager.ManageTodos;
using Prism.Modularity;
using System.Windows;
using TimeManager.Shell.Views;
using Prism.Unity;
using Microsoft.Practices.Unity;

namespace TimeManager
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ManageTodosModule));
            catalog.AddModule(typeof(ManageProjectsModule));
        }
    }
}
