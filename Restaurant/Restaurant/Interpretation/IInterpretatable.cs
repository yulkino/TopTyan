using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public interface IInterpretatable
    {
        Queue<EventData> EventQueue { get; }
        Dictionary<Event, Action<EventData>> Actions { get; }
    }
}
