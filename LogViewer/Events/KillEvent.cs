using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    class KillEvent : LogEvent
    {
        private string deathAlias;

        public KillEvent(string _eventsLine, long _eventOrder, string _alias, string _deathAlias)
        {
            eventsLine = _eventsLine;
            eventOrder = _eventOrder;
            alias = _alias;
            deathAlias = _deathAlias;
        }

        public string getDeathAlias()
        {
            return deathAlias;
        }

        public override bool Equals(object obj)
        {
            try
            {
                KillEvent toCompare = (KillEvent)obj;

                return  (toCompare.deathAlias == this.deathAlias) &&
                        base.Equals(toCompare);
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
    }
}
