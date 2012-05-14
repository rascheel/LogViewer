using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    class TmpBanEvent : LogEvent
    {
        public TmpBanEvent(string _eventsLine, long _eventOrder, string _alias)
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
            // TODO: Update get hash code to include member specific attributes
            return base.GetHashCode();
        }
    }
}
