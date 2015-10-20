using System.Xml.Linq;

namespace TimeManager.Infrastructure.Interfaces
{
    public interface IXmlable
    {
        XElement TransformToXml();
    }
}
