using PostSharp.Patterns.Model;
using System;

namespace TimeManager.ManageTodos.Models
{
    [NotifyPropertyChanged]
    public class Todo
    {
        #region properties
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime FinalDate { get; set; }
        #endregion
    }
}
