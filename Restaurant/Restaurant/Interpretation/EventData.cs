using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant
{
    public class EventData
    {
        public Event Event;
        public List<object> Data;

        public EventData(Event @event) => Event = @event;
        public EventData(Event @event, List<object> data) : this(@event) => Data = data;
    }
}
