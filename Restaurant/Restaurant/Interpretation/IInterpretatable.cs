using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public interface IInterpretatable
    {
        Queue<Event> EventQueue { get; }
        Dictionary<Event, Action> InterpretatableAction { get; }
    }
}
