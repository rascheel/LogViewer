﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    class KickEvent : LogEvent
    {
        public KickEvent(string _eventsLine, long _eventOrder, string _alias)
        {
            eventsLine = _eventsLine;
            eventOrder = _eventOrder;
            alias = _alias;

        }
    }
}
