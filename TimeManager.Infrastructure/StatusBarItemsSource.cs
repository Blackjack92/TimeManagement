using System.Collections.ObjectModel;

namespace TimeManager.Infrastructure
{
    public sealed class StatusBarItemsSource : ObservableCollection<string>
    {
        public StatusBarItemsSource()
        {
            Add("Ready");
        }
    }
}
