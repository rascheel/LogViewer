using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    public abstract class LogEvent
    {
        protected string eventsLine;
        protected long eventOrder;
        protected string alias;

        protected LogEvent()
        {
            //eventsLine = "";
            //eventOrder = 0;
        }

        public string getAlias()
        {
            return alias;
        }

        public string getEventLine()
        {
            return eventsLine;
        }

        /*public LogEvent(string _eventsLine, int _eventOrder)
        {
            eventsLine = _eventsLine;
            eventOrder = _eventOrder;
        }
        */
    }
}
