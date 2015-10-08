using Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PostSharp.Patterns.Model;

namespace TimeManager.ManageTodos.Views
{
    /// <summary>
    /// Interaktionslogik für RibbonView.xaml
    /// </summary>
    /// 
    public partial class RibbonView : IActiveAware
    {
        public event EventHandler IsActiveChanged;
        private bool isActive;

        public RibbonView()
        {
            InitializeComponent();
        }

        public bool IsActive
        {
            get { return isActive; }
            set {
                isActive = value;
                if (isActive)
                {
                    UpdateRibbonSelection();
                }
            }
        }

        private void UpdateRibbonSelection()
        {
            //if (ribbon.Items.Count > 0)
            //{
            //    //ribbon.
            //    ribbon.InitializeComponent();
            //    ribbon.SelectedItem = ribbon.Items[0];
            //    ((Crystalbyte.UI.RibbonTab)ribbon.Items[0]).IsSelected = true;
            //    ((Crystalbyte.UI.RibbonTab)ribbon.Items[0]).UpdateLayout();
            //    ((Crystalbyte.UI.RibbonTab)ribbon.Items[0]).ApplyTemplate();
            //    ((Crystalbyte.UI.RibbonTab)ribbon.Items[0]).BeginInit();

            //    UpdateDefaultStyle();
        }
    }
}
