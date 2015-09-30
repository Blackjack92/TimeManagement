using Prism.Mvvm;
using System;

namespace TimeManager.ManageTodos.Models
{
    public class Todo : BindableBase
    {
        #region fields
        private string title;
        private string description;
        private Priority priority;
        private DateTime finalDate;
        #endregion

        #region properties
        public string Title { get { return title; } set { SetProperty(ref title, value); } }
        public string Description { get { return description; } set { SetProperty(ref description, value); } }
        public Priority Priority { get { return priority; } set { SetProperty(ref priority, value); } }
        public DateTime FinalDate { get { return finalDate; } set { SetProperty(ref finalDate, value); } }
        #endregion
    }
}
