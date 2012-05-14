using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    class ChatEvent : LogEvent
    {
        public ChatEvent(string _eventsLine, long _eventOrder, string _alias)
        {
            eventsLine = _eventsLine;
            eventOrder = _eventOrder;
            alias = _alias;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }
    }
}
