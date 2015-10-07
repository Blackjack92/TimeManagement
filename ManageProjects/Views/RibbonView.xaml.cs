using Prism;
using System;

namespace TimeManager.ManageProjects.Views
{
    /// <summary>
    /// Interaktionslogik für RibbonView.xaml
    /// </summary>
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
            set
            {
                isActive = value;
                if (isActive)
                {
                    UpdateRibbonSelection();
                }
            }
        }

        private void UpdateRibbonSelection()
        {
            var r = ribbon;
            //ribbon.SelectedItem = ribbon.Items.Count > 0 ? ribbon.Items[0] : null;
        }
    }
}
