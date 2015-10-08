using System.Windows.Controls;
using System.Windows.Data;
using TimeManager.Infrastructure;

namespace TimeManager.ManageTodos.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void workingItemsDataGridTargetUpdated(object sender, DataTransferEventArgs e)
        {
            todosDataGrid.UpdateLayout();
            var workingItemsDataGrid = UIHelper.FindChild<DataGrid>(todosDataGrid, "workingItemsDataGrid");
            if (workingItemsDataGrid != null)
            {
                workingItemsDataGrid.Columns[2].Width = 0;
                workingItemsDataGrid.UpdateLayout();
                workingItemsDataGrid.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
    }
}
