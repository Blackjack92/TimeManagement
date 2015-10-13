using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using TimeManager.Infrastructure;
using TimeManager.ManageTodos.Services;
using TimeManager.ManageTodos.ViewModels;
using TimeManager.ManageTodos.Views;

namespace TimeManager.ManageTodos
{
    public class ManageTodosModule : IModule
    {
        #region fields
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        #endregion

        #region ctor
        public ManageTodosModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        #endregion

        #region methods
        public void Initialize()
        {
            container.RegisterType<ManageTodosService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ManageWorkingItemsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<DataLoadService>(new ContainerControlledLifetimeManager());
            container.RegisterType<TimerService>(new ContainerControlledLifetimeManager());

            container.RegisterType<ISelectedTodoProvider, MainViewModel>(new ContainerControlledLifetimeManager());

            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(MainView));
            regionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(RibbonView));
        }
        #endregion
    }
}
