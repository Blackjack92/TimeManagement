using TimeManager.ManageProjects;
using TimeManager.ManageTodos;
using Prism.Modularity;
using System.Windows;
using TimeManager.Shell.Views;
using Prism.Unity;
using Microsoft.Practices.Unity;
using TimeManager.Navigation;
using Prism.Mvvm;
using TimeManager.Shell.ViewModels;
using System;
using System.Reflection;
using System.Globalization;

namespace TimeManager
{
    public class Bootstrapper : UnityBootstrapper
    {
        #region methods
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) => 
            {
                if (viewType == typeof(MainWindow))
                {
                    return typeof(MainWindowModel);
                }

                var viewName = viewType.FullName;
                viewName = viewName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ManageTodosModule));
            catalog.AddModule(typeof(ManageProjectsModule));
            catalog.AddModule(typeof(NavigationModule));
        }
        #endregion
    }
}
