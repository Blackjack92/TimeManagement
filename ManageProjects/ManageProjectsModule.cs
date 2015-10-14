using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using TimeManager.Infrastructure;
using TimeManager.Infrastructure.Events;
using TimeManager.ManageProjects.Views;

namespace TimeManager.ManageProjects
{
    public class ManageProjectsModule : IModule
    {
        #region fields
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region ctor
        public ManageProjectsModule(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<StringEvent>().Subscribe(Open, ThreadOption.PublisherThread, true, filter => string.Equals(filter, ApplicationMenuNames.Open));
            this.eventAggregator.GetEvent<StringEvent>().Subscribe(Save, ThreadOption.PublisherThread, true, filter => string.Equals(filter, ApplicationMenuNames.Save));
        }
        #endregion

        #region methods
        private void Open(string file)
        {

        }

        private void Save(string file)
        {

        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(MainView));
            regionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(RibbonView));
        }
        #endregion
    }
}
