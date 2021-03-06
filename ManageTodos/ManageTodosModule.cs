﻿using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using TimeManager.Infrastructure.Events;
using TimeManager.ManageTodos.Services;
using TimeManager.ManageTodos.ViewModels;
using TimeManager.ManageTodos.Views;
using TimeManager.Infrastructure.Names;
using TimeManager.ManageTodos.Models;
using TimeManager.Infrastructure.Data;
using System.Collections.Generic;

namespace TimeManager.ManageTodos
{
    public class ManageTodosModule : IModule
    {
        #region fields
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private readonly IEventAggregator eventAggregator;
        #endregion

        #region ctor
        public ManageTodosModule(IRegionManager regionManager, IUnityContainer container, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.container = container;
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
            container.RegisterType<TodosRoot>(new ContainerControlledLifetimeManager());
            container.RegisterType<WorkingItemsRoot>(new ContainerControlledLifetimeManager());
            //container.RegisterType<DataLoadService>(new ContainerControlledLifetimeManager());
            container.RegisterType<TimerService>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<IStoreRoot>("TodosStoreRoot", container.Resolve<TodosRoot>());
            container.RegisterType<IEnumerable<IStoreRoot>, IStoreRoot[]>();

            container.RegisterType<ISelectedTodoProvider, MainViewModel>(new ContainerControlledLifetimeManager());

            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(MainView));
            regionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(RibbonView));
        }
        #endregion
    }
}
