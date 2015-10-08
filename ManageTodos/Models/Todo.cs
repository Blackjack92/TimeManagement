using PostSharp.Patterns.Model;
using System;
using System.Collections.ObjectModel;
using TimeManager.Infrastructure;

namespace TimeManager.ManageTodos.Models
{
    [NotifyPropertyChanged]
    public class Todo
    {
        public static class TodoProperties
        {
            public static string Done = StaticReflection.GetMemberName<Todo>(todo => todo.Done);
        }

        #region properties
        public Guid Id { get; set; }
        public bool Done { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime FinalDate { get; set; }
        public ObservableCollection<WorkingItem> WorkingItems { get; set; }
        #endregion

        #region ctor
        public Todo()
        {
            WorkingItems = new ObservableCollection<WorkingItem>();
        }
        #endregion
    }
}
