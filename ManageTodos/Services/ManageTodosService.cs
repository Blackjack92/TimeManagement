using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using TimeManager.ManageTodos.Models;
using TimeManager.ManageTodos.Properties;

namespace TimeManager.ManageTodos.Services
{
    public class ManageTodosService
    {
        #region fields
        private ObservableCollection<Todo> todos;
        #endregion

        #region properties
        public ObservableCollection<Todo> Todos { get { return todos; } private set { todos = value; } }
        #endregion

        #region ctor
        public ManageTodosService()
        {
            todos = new ObservableCollection<Todo>();
        }
        #endregion

        #region methods
        private void ReadTodos()
        {
            var document = XDocument.Parse(Resources.Todos);

            //var newsData = document.Descendants("NewsItem")
            //    .GroupBy(x => x.Attribute("TickerSymbol").Value,
            //    x => new NewsArticle
            //    {
            //        PublishedDate = DateTime.Parse(x.Attribute("PublishedDate").Value, CultureInfo.InvariantCulture),
            //        Title = x.Element("Title").Value,
            //        Body = x.Element("Body").Value,
            //        IconUri = x.Attribute("IconUri") != null ? x.Attribute("IconUri").Value : null
            //    })
            //    .ToDictionary(group => group.Key, group => group.ToList());
        }
        #endregion
    }
}
