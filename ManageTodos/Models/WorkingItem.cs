using System;
using System.Xml.Linq;
using PostSharp.Patterns.Model;
using TimeManager.Infrastructure.Interfaces;

namespace TimeManager.ManageTodos.Models
{

    [NotifyPropertyChanged]
    public class WorkingItem : IXmlable
    {
        #region properties
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public TimeSpan SpentTime
        {
            get
            {
                if (Depends.Guard)
                {
                    Depends.On(Start, End);
                }
                return End.Subtract(Start);
            }
        }
        #endregion

        #region methods
        public XElement TransformToXml()
        {
            return null;
        }
        #endregion
    }
}
