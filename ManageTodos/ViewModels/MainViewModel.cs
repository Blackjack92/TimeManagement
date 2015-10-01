using Prism.Mvvm;
using TimeManager.ManageTodos.Services;

namespace TimeManager.ManageTodos.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region fields
        private readonly ManageTodosService manageTodosService;
        private readonly ManageWorkingItemsService manageWorkingItemsService;
        #endregion

        #region properties
        public ManageTodosService ManageTodosService { get { return manageTodosService; } }
        public ManageWorkingItemsService ManageWorkingItemsService { get { return manageWorkingItemsService; } }
        #endregion

        #region ctor
        public MainViewModel(ManageTodosService manageTodosService, ManageWorkingItemsService manageWorkingItemsService, DataLoadService dataLoadService)
        {
            this.manageTodosService = manageTodosService;
            this.manageWorkingItemsService = manageWorkingItemsService;
            dataLoadService.LoadData();
        }
        #endregion
    }
}
