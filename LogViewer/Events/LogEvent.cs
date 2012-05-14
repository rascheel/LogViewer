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
        protected DateTime eventDateTime;

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

        public override bool Equals(object obj)
        {
            try
            {
                LogEvent le = (LogEvent)obj;

                return  le.alias == this.alias &&
                        le.eventOrder == this.eventOrder &&
                        le.eventsLine == this.eventsLine;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            // TODO: Update get hash code to include member specific attributes
            return base.GetHashCode();
        }

        /*public LogEvent(string _eventsLine, int _eventOrder)
        {
            eventsLine = _eventsLine;
            eventOrder = _eventOrder;
        }
        */
    }
}
