using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    class LoginEvent : LogEvent
    {
        protected string uID;
        
        private LoginEvent()
        {
        }

        public LoginEvent(string _eventsLine, long _eventOrder, string _alias, string _uID) 
        {
            eventsLine = _eventsLine;
            eventOrder = _eventOrder;
            uID = _uID;
            alias = _alias;
        }

        public string getUID()
        {
            return uID;
        }

        public override bool Equals(object obj)
        {
            try
            {
                LoginEvent le = (LoginEvent)obj;
                return  le.uID == this.uID &&
                        base.Equals(obj);
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
