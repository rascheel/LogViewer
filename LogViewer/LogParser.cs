using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    static class LogParser
    {
        static long eventOrder = 0;

        public static LogEvent parseEvent(DateTime date, string eventLine)
        {
            eventOrder++;
            eventLine = eventLine.Trim();
            char[] eventLineArray = eventLine.ToCharArray();
            if (eventLineArray.Length > 0)
            {
                if ((eventLineArray[11] == '[' || (eventLine.Contains("*DEAD*") && eventLineArray[11] == '*')) && eventLine.Contains("] "))
                {
                    string alias = "";


                    int i;
                    if (eventLineArray[11] != '*')
                    {
                        i = 12;
                    }
                    else
                    {
                        i = 19;
                    }

                    while (eventLineArray[i] != ']')
                    {
                        alias += eventLineArray[i];
                        i++;
                    }

                    return new ChatEvent(eventLine, eventOrder, alias);

                }
                else if (eventLine.Contains(" has joined the game with ID: "))
                {


                    string alias = "";
                    string uID = "";


                    string tempEventLine = eventLine.Substring(11);

                    alias = tempEventLine.Substring(0, tempEventLine.IndexOf(" has joined the game with ID: "));
                    alias = alias.Trim();

                    tempEventLine = tempEventLine.Substring(tempEventLine.IndexOf("ID: ") + 4);

                    int i = 0;
                    while (i < tempEventLine.Length && tempEventLine[i] != ' ')
                    {
                        uID += tempEventLine[i];
                        i++;
                    }

                    return new LoginEvent(eventLine, eventOrder, alias, uID);

                }
                else if (eventLine.Contains("<img"))
                {
                    string alias = "";
                    string deathAlias = "";

                    if (eventLineArray[12] != '<') //if not suicide
                    {
                        int i = 11;

                        while (eventLineArray[i] != '<')
                        {
                            alias += eventLineArray[i];
                            i++;
                        }

                        alias = alias.Trim();

                        while (eventLineArray[i] != '>')
                        {
                            i++;
                        }
                        i += 2;

                        while (i < eventLineArray.Length)
                        {
                            deathAlias += eventLineArray[i];
                            i++;
                        }

                        return new KillEvent(eventLine, eventOrder, alias, deathAlias);
                    }
                    else //else suicide
                    {
                        int i = 12;

                        while (eventLineArray[i] != '>')
                        {
                            i++;
                        }
                        i += 2;

                        while (i < eventLineArray.Length)
                        {
                            deathAlias += eventLineArray[i];
                            i++;
                        }

                        return new KillEvent(eventLine, eventOrder, alias, deathAlias);
                    }

                }
                else if (eventLine.Contains("started a poll to ban") || eventLine.Contains("started a poll to kick"))
                {
                    string alias = "";

                    int i = 11;
                    while (eventLineArray[i] != ' ')
                    {
                        alias += eventLineArray[i];
                        i++;
                    }

                    return new KickBanPollEvent(eventLine, eventOrder, alias);

                }
                else if (eventLine.Contains("started a poll to change"))
                {
                    string alias = "";

                    int i = 11;
                    while (eventLineArray[i] != ' ')
                    {
                        alias += eventLineArray[i];
                        i++;
                    }

                    return new MapPollEvent(eventLine, eventOrder, alias);
                }
                else if(eventLine.Contains("is kicked"))
                {
                    string alias = "";

                    int i = 11;
                    while (eventLineArray[i] != ' ')
                    {
                        alias += eventLineArray[i];
                        i++;
                    }

                    return new KickEvent(eventLine, eventOrder, alias);

                }
                else if(eventLine.Contains("is banned temporarily"))
                {
                    string alias = "";

                    int i = 11;
                    while (eventLineArray[i] != ' ')
                    {
                        alias += eventLineArray[i];
                        i++;
                    }

                    return new TmpBanEvent(eventLine, eventOrder, alias);
                }
                else if(eventLine.Contains("banned permanently"))
                {
                    string alias = "";

                    int i = 11;
                    while (eventLineArray[i] != ' ')
                    {
                        alias += eventLineArray[i];
                        i++;
                    }

                    return new PermaBanEvent(eventLine, eventOrder, alias);
                }

            }
            return null;

                

            
        }
    }
}

