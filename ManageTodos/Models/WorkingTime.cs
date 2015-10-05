using System;
using PostSharp.Patterns.Model;

namespace TimeManager.ManageTodos.Models
{
    [NotifyPropertyChanged]
    public class WorkingTime
    {
        #region properties
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        #endregion
    }
}
