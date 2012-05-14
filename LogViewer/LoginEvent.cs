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

        public LoginEvent(string _eventsLine, long _eventOrder, string _uID, string _alias) 
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

        
    }
}
