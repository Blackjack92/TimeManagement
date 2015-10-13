using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Infrastructure.Events
{
    public class StringEvent : PubSubEvent<string>
    {
        //: CompositePresentationEvent<string>
    }
}
