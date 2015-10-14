using System.Collections.ObjectModel;

namespace TimeManager.Infrastructure.Utils
{
    public sealed class StatusBarItemsSource : ObservableCollection<string>
    {
        public StatusBarItemsSource()
        {
            Add("Ready");
        }
    }
}
